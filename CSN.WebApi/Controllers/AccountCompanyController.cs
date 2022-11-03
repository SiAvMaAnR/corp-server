using CSN.Domain.Entities.Companies;
using CSN.Infrastructure.Helpers;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.AccCompany;
using CSN.Persistence.DBContext;
using CSN.WebApi.Extensions.CustomExceptions;
using CSN.WebApi.Models.AccCompany;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountCompanyController : ControllerBase
    {
        private readonly EFContext eFContext;
        private readonly IAccCompanyService accCompanyService;
        private readonly ILogger<AccountCompanyController> logger;

        public AccountCompanyController(EFContext eFContext, IAccCompanyService accCompanyService, ILogger<AccountCompanyController> logger)
        {
            this.eFContext = eFContext;
            this.accCompanyService = accCompanyService;
            this.logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AccCompanyLogin request)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Data is not correct");
            }

            var response = await accCompanyService.LoginAsync(new AccCompanyLoginRequest()
            {
                Email = request.Email,
                Password = request.Password
            });

            return Ok(new
            {
                response.IsSuccess,
                response.TokenType,
                response.Token
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccCompanyRegister request)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Data is not correct");
            }

            string role = "Company";

            var response = await accCompanyService.RegisterAsync(new AccCompanyRegisterRequest()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Image = request.Image,
                Description = request.Description,
                Role = role
            });

            return Ok(new
            {
                isSuccess = response.IsSuccess
            });
        }

        [HttpGet("Info"), Authorize(Roles = "Company")]
        public async Task<IActionResult> Info()
        {
            var response = await accCompanyService.InfoAsync(new AccCompanyInfoRequest());

            return Ok(new
            {
                response.Id,
                response.Name,
                response.Email,
                response.Role,
                response.Description,
                response.Image
            });
        }
    }
}
