using CSN.Domain.Entities.Companies;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Helpers;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.AccCompany;
using CSN.Persistence.DBContext;
using CSN.WebApi.Extensions;
using CSN.WebApi.Extensions.CustomExceptions;
using CSN.WebApi.Services.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSN.WebApi.Services
{
    public class AccCompanyService : BaseService<Company>, IAccCompanyService
    {
        private readonly IConfiguration configuration;

        public AccCompanyService(EFContext eFContext, IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration)
            : base(eFContext, unitOfWork, context)
        {
            this.configuration = configuration;
        }

        public async Task<AccCompanyLoginResponse> LoginAsync(AccCompanyLoginRequest request)
        {
            Company? company = await unitOfWork.Company.GetAsync(company => company.Email == request.Email);

            if (company == null)
            {
                throw new NotFoundException("Company not found");
            }

            bool isVerify = AuthOptions.VerifyPasswordHash(request.Password, company.PasswordHash, company.PasswordSalt);

            if (!isVerify)
            {
                throw new BadRequestException("Incorrect email or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, company.Id.ToString()),
                new Claim(ClaimTypes.Name, company.Name),
                new Claim(ClaimTypes.Email, company.Email),
                new Claim(ClaimTypes.Role, company.Role)
            };

            string token = AuthOptions.CreateToken(claims, new Dictionary<string, string>()
            {
                { "secretKey", configuration.GetSection("Authorization:SecretKey").Value },
                { "audience", configuration.GetSection("Authorization:Audience").Value },
                { "issuer" , configuration.GetSection("Authorization:Issuer").Value },
                { "lifeTime" , configuration.GetSection("Authorization:LifeTime").Value },
            });


            return new AccCompanyLoginResponse()
            {
                IsSuccess = true,
                TokenType = "Bearer",
                Token = token
            };
        }

        public async Task<AccCompanyRegisterResponse> RegisterAsync(AccCompanyRegisterRequest request)
        {
            if (await unitOfWork.Company.AnyAsync(company => company.Email == request.Email))
            {
                throw new BadRequestException("User already exists");
            }

            if (!AuthOptions.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt))
            {
                throw new BadRequestException("Incorrect password");
            }

            var image = Convert.FromBase64String(request.Image ?? "");

            await unitOfWork.Company.AddAsync(new Company()
            {
                Name = request.Name,
                Email = request.Email,
                Image = image,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Description = request.Description,

            });

            await unitOfWork.SaveChangesAsync();

            return new AccCompanyRegisterResponse()
            {
                IsSuccess = true
            };
        }

        public async Task<AccCompanyInfoResponse> InfoAsync(AccCompanyInfoRequest request)
        {

            var company = await claimsPrincipal.GetCompanyAsync(unitOfWork);

            if (company == null)
            {
                throw new NotFoundException("User is not found");
            }

            return new AccCompanyInfoResponse()
            {
                Id = company.Id,
                Name = company.Name,
                Email = company.Email,
                Image = company.Image,
                Role = company.Role,
                Description = company.Description,
                Employees = company.Employees
            };
        }
    }
}
