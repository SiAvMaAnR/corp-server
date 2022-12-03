using CSN.Domain.Entities.Companies;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Email;
using CSN.Email.Models;
using CSN.Infrastructure.Helpers;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.Common;
using CSN.Infrastructure.Models.CompanyDto;
using CSN.WebApi.Extensions;
using CSN.WebApi.Extensions.CustomExceptions;
using CSN.WebApi.Services.Common;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Claims;
using System.Text.Json;

namespace CSN.WebApi.Services
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        private readonly IConfiguration configuration;
        public readonly IDataProtectionProvider protection;
        private readonly EmailClient emailClient;

        public CompanyService(IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration,
            IDataProtectionProvider protection) : base(unitOfWork, context)
        {
            this.configuration = configuration;
            this.protection = protection;

            this.emailClient = new EmailClient(new MessageHandler(new SmtpModel()
            {
                Email = this.configuration["Smtp:Email"],
                Password = this.configuration["Smtp:Password"],
                Host = this.configuration["Smtp:Host"],
                Port = int.Parse(this.configuration["Smtp:Port"]),
            }));
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

            string token = AuthOptions.CreateToken(claims, new Dictionary<string, string>()
            {
                { "secretKey", this.configuration["Authorization:SecretKey"] },
                { "audience", this.configuration["Authorization:Audience"] },
                { "issuer" , this.configuration["Authorization:Issuer"] },
                { "lifeTime" , this.configuration["Authorization:LifeTime"] },
            });


            return new CompanyLoginResponse()
            {
                IsSuccess = true,
                TokenType = "Bearer",
                Token = token
            };
        }

        public async Task<CompanyRegisterResponse> RegisterAsync(CompanyRegisterRequest request)
        {
            if (await this.unitOfWork.Company.AnyAsync(company => company.Email == request.Email))
            {
                throw new BadRequestException("Account already exists");
            }

            if (await this.unitOfWork.Employee.AnyAsync(employee => employee.Email == request.Email))
            {
                throw new BadRequestException("Account already exists");
            }

            byte[] image = Convert.FromBase64String(request.Image ?? "");

            string baseUrl = this.configuration["Invite:BaseUrl"];
            string path = this.configuration["Invite:Path"];

            IDataProtector protector = this.protection.CreateProtector(this.configuration["Invite:SecretKey"]);

            string confirmationJson = JsonSerializer.Serialize(new Confirmation()
            {
                Login = request.Login,
                Email = request.Email,
                Image = image,
                Password = request.Password,
                Description = request.Description
            });

            string confirmation = protector.Protect(confirmationJson);

            string message = $"{baseUrl}/{path}?confirm={confirmation}";

            await this.emailClient.SendAsync(new MessageModel()
            {
                From = new AddressModel(baseUrl, this.configuration["Smtp:Email"]),
                To = new AddressModel(request.Login, request.Email),
                Subject = $"Welcome, verify your account. To do this, follow the link!",
                Message = message
            });

            return new CompanyRegisterResponse()
            {
                IsSuccess = true
            };
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
                Description = company.Description,
            };
        }

        public async Task<CompanyConfirmationResponse> ConfirmAccountAsync(CompanyConfirmationRequest request)
        {
            var secretKey = this.configuration["Invite:SecretKey"];

            IDataProtector protector = this.protection.CreateProtector(secretKey);
            string confirmationJson = protector.Unprotect(request.Confirmation);

            Confirmation? confirmation = JsonSerializer.Deserialize<Confirmation>(confirmationJson);

            if (confirmation == null)
            {
                throw new BadRequestException("Incorrect confirmation");
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

            await this.unitOfWork.Company.AddAsync(company);

            await this.unitOfWork.SaveChangesAsync();

            return new CompanyConfirmationResponse()
            {
                IsSuccess = true
            };
        }
    }
}
