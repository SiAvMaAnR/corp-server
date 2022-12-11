using System.Linq;
using CSN.Domain.Entities.Companies;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.CompanyDto;
using CSN.Persistence.DBContext;
using CSN.WebApi.Extensions.CustomExceptions;
using CSN.WebApi.Models.Company;
using CSN.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly ILogger<CompanyController> logger;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            this.companyService = companyService;
            this.logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] CompanyLogin request)
        {
            var response = await this.companyService.LoginAsync(new CompanyLoginRequest()
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
        public async Task<IActionResult> Register([FromBody] CompanyRegister request)
        {
            var response = await this.companyService.RegisterAsync(new CompanyRegisterRequest()
            {
                Login = request.Login,
                Email = request.Email,
                Password = request.Password,
                Image = request.Image,
                Description = request.Description,
            });

            return Ok(new
            {
                response.IsSuccess
            });
        }


        [HttpPost("Confirm")]
        public async Task<IActionResult> Confirmation([FromBody] CompanyConfirm request)
        {
            var response = await this.companyService.ConfirmAccountAsync(new CompanyConfirmationRequest()
            {
                Confirmation = request.Confirmation
            });

            return Ok(new
            {
                response.IsSuccess
            });
        }

        [HttpGet("Info"), Authorize(Roles = "Company")]
        public async Task<IActionResult> Info()
        {
            var response = await this.companyService.GetInfoAsync(new CompanyInfoRequest());

            return Ok(new
            {
                response.Id,
                response.Login,
                response.Email,
                response.Role,
                response.Description,
                response.Image
            });
        }
    }
}
