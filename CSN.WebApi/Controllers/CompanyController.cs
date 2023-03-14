using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.CompanyDto;
using CSN.WebApi.Models.Company;
using Microsoft.AspNetCore.Authorization;
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
            var response = await companyService.LoginAsync(new CompanyLoginRequest()
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
            var response = await companyService.RegisterAsync(new CompanyRegisterRequest()
            {
                Login = request.Login,
                Email = request.Email,
                Password = request.Password,
                Description = request.Description,
            });

            return Ok(new
            {
                response.IsSuccess
            });
        }

        [HttpPost("Edit"), Authorize(Policy = "OnlyCompany")]
        public async Task<IActionResult> Edit([FromBody] CompanyEdit request)
        {
            var response = await companyService.EditAsync(new CompanyEditRequest()
            {
                Login = request.Login,
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
            var response = await companyService.ConfirmAccountAsync(new CompanyConfirmationRequest()
            {
                Confirmation = request.Confirmation
            });

            return Ok(new
            {
                response.IsSuccess
            });
        }

        [HttpGet("Info"), Authorize(Policy = "OnlyCompany")]
        public async Task<IActionResult> Info()
        {
            var response = await companyService.GetInfoAsync(new CompanyInfoRequest());

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
