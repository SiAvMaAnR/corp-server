using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Helpers;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.AccEmployeeDto;
using CSN.Persistence.DBContext;
using CSN.WebApi.Extensions;
using CSN.WebApi.Extensions.CustomExceptions;
using CSN.WebApi.Services.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSN.WebApi.Services
{
    public class AccEmployeeService : BaseService<Employee>, IAccEmployeeService
    {
        private readonly IConfiguration configuration;

        public AccEmployeeService(EFContext eFContext, IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration)
            : base(eFContext, unitOfWork, context)
        {
            this.configuration = configuration;
        }

        public async Task<AccEmployeeLoginResponse> LoginAsync(AccEmployeeLoginRequest request)
        {
            Employee? employee = await unitOfWork.Employee.GetAsync(employee => employee.Email == request.Email);

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
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim(ClaimTypes.Role, employee.Role)
            };

            string token = AuthOptions.CreateToken(claims, new Dictionary<string, string>()
            {
                { "secretKey", configuration.GetSection("Authorization:SecretKey").Value },
                { "audience", configuration.GetSection("Authorization:Audience").Value },
                { "issuer" , configuration.GetSection("Authorization:Issuer").Value },
                { "lifeTime" , configuration.GetSection("Authorization:LifeTime").Value },
            });


            return new AccEmployeeLoginResponse()
            {
                IsSuccess = true,
                TokenType = "Bearer",
                Token = token
            };
        }

        public async Task<AccEmployeeRegisterResponse> RegisterAsync(AccEmployeeRegisterRequest request)
        {
            if (await unitOfWork.Employee.AnyAsync(employee => employee.Email == request.Email))
            {
                throw new BadRequestException("Account already exists");
            }

            if (!AuthOptions.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt))
            {
                throw new BadRequestException("Incorrect password");
            }

            var image = Convert.FromBase64String(request.Image ?? "");

            Company? company = await unitOfWork.Company.GetAsync(company => company.Id == request.CompanyId);

            if (company == null)
            {
                throw new BadRequestException("Incorrect company");
            }

            var employee = new Employee()
            {
                Login = request.Login,
                Email = request.Email,
                Image = image,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = request.Role,
                CompanyId = request.CompanyId,
            };

            await unitOfWork.Employee.AddAsync(employee);

            await unitOfWork.SaveChangesAsync();

            return new AccEmployeeRegisterResponse()
            {
                IsSuccess = true
            };
        }

        public async Task<AccEmployeeInfoResponse> InfoAsync(AccEmployeeInfoRequest request)
        {
            Employee? employee = await claimsPrincipal!.GetEmployeeAsync(unitOfWork, employee => employee.Company);

            if (employee == null)
            {
                throw new NotFoundException("Account is not found");
            }

            return new AccEmployeeInfoResponse()
            {
                Id = employee.Id,
                Login = employee.Login,
                Email = employee.Email,
                Image = employee.Image,
                Role = employee.Role,
                CompanyId = employee.CompanyId,
                Company = employee.Company
            };
        }
    }
}
