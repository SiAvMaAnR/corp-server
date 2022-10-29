using CSN.Domain.Entities.Companies;
using CSN.Infrastructure.Helpers;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.AccCompany;
using CSN.Persistence.DBContext;
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
            User
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AccCompanyLogin request)
        {
            try
            {
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccCompanyRegister request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Model not valid");
                }


                var response = await accCompanyService.RegisterAsync(new AccCompanyRegisterRequest()
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = request.Password,
                    Image = request.Image,
                    Description = request.Description,
                });

                return Ok(new
                {
                    isSuccess = response.IsSuccess
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Info"), Authorize]
        public IActionResult Info()
        {
            return Ok(new
            {
                user = User.Identity
            });
        }
    }
}
