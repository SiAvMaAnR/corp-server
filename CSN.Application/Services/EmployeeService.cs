using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.EmployeeDto;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.AuthOptions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text.Json;
using CSN.Domain.Exceptions;
using CSN.Application.Extensions;
using CSN.Persistence.Extensions;
using CSN.Domain.Entities.Invitations;

namespace CSN.Application.Services;

public class EmployeeService : BaseService, IEmployeeService
{
    public readonly IConfiguration configuration;
    public readonly IDataProtectionProvider protection;

    public EmployeeService(IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration,
        IDataProtectionProvider protection) : base(unitOfWork, context)
    {
        this.configuration = configuration;
        this.protection = protection;
    }

    public async Task<EmployeeLoginResponse> LoginAsync(EmployeeLoginRequest request)
    {
        Employee? employee = await this.unitOfWork.Employee.GetAsync(employee => employee.Email == request.Email);

        if (employee == null)
        {
            throw new NotFoundException("Account not found");
        }

        bool isVerify = AuthOptions.VerifyPasswordHash(request.Password, employee.PasswordHash, employee.PasswordSalt);

        if (!isVerify)
        {
            throw new BadRequestException("Incorrect email or password");
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
            new Claim(ClaimTypes.Name, employee.Login),
            new Claim("Post", employee.Post.ToString()),
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim(ClaimTypes.Role, employee.Role)
        };

        string token = AuthOptions.CreateToken(claims, new Dictionary<string, string>()
        {
            { "secretKey", this.configuration["Authorization:SecretKey"] ?? throw new BadRequestException("Missing authorization secretKey") },
            { "audience", this.configuration["Authorization:Audience"] ?? throw new BadRequestException("Missing authorization audience") },
            { "issuer" , this.configuration["Authorization:Issuer"] ?? throw new BadRequestException("Missing authorization issuer") },
            { "lifeTime" , this.configuration["Authorization:LifeTime"] ?? throw new BadRequestException("Missing authorization lifeTime") },
        });

        return new EmployeeLoginResponse()
        {
            IsSuccess = true,
            TokenType = "Bearer",
            Token = token
        };
    }

    public async Task<EmployeeRegisterResponse> RegisterAsync(EmployeeRegisterRequest request)
    {
        var secretKey = this.configuration["Invite:SecretKey"] ?? throw new BadRequestException("Missing invite secretKey");

        IDataProtector protector = this.protection.CreateProtector(secretKey);
        string inviteJson = protector.Unprotect(request.Invite);

        Invite? invite = JsonSerializer.Deserialize<Invite>(inviteJson);

        if (invite == null)
        {
            throw new BadRequestException("Incorrect invite");
        }

        if (await this.unitOfWork.User.AnyAsync(user => user.Email == invite.Email))
        {
            throw new BadRequestException("Account already exists");
        }

        Invitation? invitation = await this.unitOfWork.Invitation.GetAsync(invitation => invitation.Id == invite.Id);

        if (invitation == null || !invitation.IsActive)
        {
            throw new NotFoundException("Invitation not found or inactive");
        }

        if (!AuthOptions.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt))
        {
            throw new BadRequestException("Incorrect password");
        }

        byte[] imageBytes = Convert.FromBase64String(request.Image ?? "");
        string? imagePath = await imageBytes.WriteToFileAsync(invite.Email);
        Company? company = await this.unitOfWork.Company.GetAsync(company => company.Id == invite.CompanyId);

        if (company == null)
        {
            throw new BadRequestException("Incorrect company");
        }

        var employee = new Employee()
        {
            Login = request.Login,
            Email = invite.Email,
            Image = imagePath,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = "Employee",
            Post = invite.Post,
            CompanyId = invite.CompanyId,
        };

        invitation.IsAccepted = true;
        invitation.IsActive = false;

        await Task.WhenAll(
            this.unitOfWork.Invitation.UpdateAsync(invitation),
            this.unitOfWork.Employee.AddAsync(employee)
        );

        await this.unitOfWork.SaveChangesAsync();

        return new EmployeeRegisterResponse()
        {
            IsSuccess = true,
            Email = invitation.Email,
            Password = request.Password,
        };
    }

    public async Task<EmployeeEditResponse> EditAsync(EmployeeEditRequest request)
    {
        Employee? employee = await this.claimsPrincipal!.GetEmployeeAsync(unitOfWork);

        if (employee == null)
        {
            throw new NotFoundException("Account is not found");
        }


        var imageBytes = Convert.FromBase64String(request.Image ?? "");
        var imagePath = await imageBytes.WriteToFileAsync(employee.Email);
        employee.Login = request.Login;
        employee.Image = imagePath;

        await this.unitOfWork.Employee.UpdateAsync(employee);
        await this.unitOfWork.SaveChangesAsync();

        return new EmployeeEditResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<EmployeeInfoResponse> GetInfoAsync(EmployeeInfoRequest request)
    {
        Employee? employee = await this.claimsPrincipal!.GetEmployeeAsync(this.unitOfWork, employee => employee.Company);

        if (employee == null)
        {
            throw new NotFoundException("Account is not found");
        }

        return new EmployeeInfoResponse()
        {
            Id = employee.Id,
            Login = employee.Login,
            Email = employee.Email,
            Image = employee.Image.ReadToBytes(),
            Role = employee.Role,
            State = employee.State,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt,
            CompanyId = employee.CompanyId,
            Company = new EmployeeCompany()
            {
                Id = employee.Company.Id,
                Login = employee.Company.Login,
                Email = employee.Company.Email,
                Role = employee.Company.Role,
                Image = employee.Company.Image.ReadToBytes(),
                State = employee.Company.State,
                Description = employee.Company.Description
            }
        };
    }

    public async Task<EmployeeRemoveResponse> RemoveAsync(EmployeeRemoveRequest request)
    {
        Employee? employee = await this.claimsPrincipal!.GetEmployeeAsync(this.unitOfWork);

        if (employee == null)
        {
            throw new NotFoundException("Account is not found");
        }

        await this.unitOfWork.Employee.DeleteAsync(employee);
        await this.unitOfWork.SaveChangesAsync();

        return new EmployeeRemoveResponse()
        {
            IsSuccess = true
        };
    }
}
