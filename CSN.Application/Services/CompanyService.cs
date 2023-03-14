using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.Common;
using CSN.Application.Services.Models.CompanyDto;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Exceptions;
using CSN.Email;
using CSN.Email.Handlers;
using CSN.Email.Models;
using CSN.Infrastructure.AuthOptions;
using CSN.Infrastructure.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text.Json;

namespace CSN.Application.Services;

public class CompanyService : BaseService<Company>, ICompanyService
{
    private readonly IConfiguration configuration;
    public readonly IDataProtectionProvider protection;
    private readonly EmailClient emailClient;

    public CompanyService(IUnitOfWork unitOfWork, IHttpContextAccessor context,
        IConfiguration configuration, IDataProtectionProvider protection) : base(unitOfWork, context)
    {
        this.configuration = configuration;
        this.protection = protection;

        var smtpModel = new SmtpModel()
        {
            Email = this.configuration["Smtp:Email"] ?? throw new BadRequestException("Missing smtp email"),
            Password = this.configuration["Smtp:Password"] ?? throw new BadRequestException("Missing smtp password"),
            Host = this.configuration["Smtp:Host"] ?? throw new BadRequestException("Missing smtp host"),
            Port = int.Parse(this.configuration["Smtp:Port"] ?? throw new BadRequestException("Missing smtp port")),
        };

        this.emailClient = new EmailClient(new MessageHandler(smtpModel));
    }

    public async Task<CompanyLoginResponse> LoginAsync(CompanyLoginRequest request)
    {
        Company? company = await this.unitOfWork.Company.GetAsync(company => company.Email == request.Email);

        if (company == null)
        {
            throw new NotFoundException("Account not found");
        }

        bool isVerify = AuthOptions.VerifyPasswordHash(request.Password, company.PasswordHash, company.PasswordSalt);

        if (!isVerify)
        {
            throw new BadRequestException("Incorrect email or password");
        }

        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, company.Id.ToString()),
                new Claim(ClaimTypes.Name, company.Login),
                new Claim(ClaimTypes.Email, company.Email),
                new Claim(ClaimTypes.Role, company.Role)
            };

        string token = AuthOptions.CreateToken(
            claims,
            new Dictionary<string, string>()
            {
                    { "secretKey", this.configuration["Authorization:SecretKey"]
                        ?? throw new BadRequestException("Missing authorization secretKey") },
                    { "audience", this.configuration["Authorization:Audience"]
                        ?? throw new BadRequestException("Missing authorization audience") },
                    { "issuer", this.configuration["Authorization:Issuer"]
                        ?? throw new BadRequestException("Missing authorization issuer") },
                    { "lifeTime", this.configuration["Authorization:LifeTime"]
                        ?? throw new BadRequestException("Missing authorization lifeTime") },
            }
        );

        return new CompanyLoginResponse()
        {
            IsSuccess = true,
            TokenType = "Bearer",
            Token = token
        };
    }

    public async Task<CompanyRegisterResponse> RegisterAsync(CompanyRegisterRequest request)
    {
        if (await this.unitOfWork.User.AnyAsync(user => user.Email == request.Email))
        {
            throw new BadRequestException("Account already exists");
        }

        string baseUrl = this.configuration["Client:BaseUrl"] ?? throw new BadRequestException("Missing client baseUrl");
        string path = this.configuration["Confirm:Path"] ?? throw new BadRequestException("Missing confirm path");
        string secretKey = this.configuration["Confirm:SecretKey"] ?? throw new BadRequestException("Missing confirm secretKey");

        IDataProtector protector = this.protection.CreateProtector(secretKey);

        string confirmationJson = JsonSerializer.Serialize(
            new Confirmation()
            {
                Login = request.Login,
                Email = request.Email,
                Password = request.Password,
                Description = request.Description
            }
        );

        string confirmation = protector.Protect(confirmationJson);

        string message = $"{baseUrl}/{path}?code={confirmation}";

        string smtpEmail = this.configuration["Smtp:Email"] ?? throw new BadRequestException("Missing smtp email");

        var messageModel = new MessageModel()
        {
            From = new AddressModel(baseUrl, smtpEmail),
            To = new AddressModel(request.Login, request.Email),
            Subject = $"Welcome, verify your account. To do this, follow the link!",
            Message = message
        };

        this.emailClient.SendAsync(messageModel).Ignore();

        return new CompanyRegisterResponse() { IsSuccess = true };
    }

    public async Task<CompanyEditResponse> EditAsync(CompanyEditRequest request)
    {
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(unitOfWork);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        company.Login = request.Login;
        company.Image = Convert.FromBase64String(request.Image ?? "");
        company.Description = request.Description;

        await this.unitOfWork.Company.UpdateAsync(company);
        await this.unitOfWork.SaveChangesAsync();

        return new CompanyEditResponse() { IsSuccess = true };
    }

    public async Task<CompanyInfoResponse> GetInfoAsync(CompanyInfoRequest request)
    {
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(unitOfWork);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        return new CompanyInfoResponse()
        {
            Id = company.Id,
            Login = company.Login,
            Email = company.Email,
            Image = company.Image,
            Role = company.Role,
            State = company.State,
            Description = company.Description,
        };
    }

    public async Task<CompanyConfirmationResponse> ConfirmAccountAsync(CompanyConfirmationRequest request)
    {
        var secretKey = this.configuration["Invite:SecretKey"] ?? throw new BadRequestException("Missing invite secretKey");

        IDataProtector protector = this.protection.CreateProtector(secretKey);
        string confirmationJson = protector.Unprotect(request.Confirmation);

        Confirmation? confirmation = JsonSerializer.Deserialize<Confirmation>(confirmationJson);

        if (confirmation == null)
        {
            throw new BadRequestException("Incorrect confirmation");
        }

        if (await this.unitOfWork.Company.AnyAsync(company => company.Email == confirmation.Email))
        {
            throw new BadRequestException("Account already exists");
        }

        if (!AuthOptions.CreatePasswordHash(confirmation.Password, out byte[] passwordHash, out byte[] passwordSalt))
        {
            throw new BadRequestException("Incorrect password");
        }

        Company company = new Company()
        {
            Login = confirmation.Login,
            Email = confirmation.Email,
            Image = confirmation.Image,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Description = confirmation.Description,
            Role = "Company",
        };

        if (await this.unitOfWork.Company.AnyAsync(company => company.Email == confirmation.Email))
        {
            throw new BadRequestException("Account already exists");
        }
        await this.unitOfWork.Company.AddAsync(company);

        await this.unitOfWork.SaveChangesAsync();

        return new CompanyConfirmationResponse() { IsSuccess = true };
    }
}
