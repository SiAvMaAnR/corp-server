using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.AccEmployeeDto;
using CSN.Persistence.DBContext;
using CSN.WebApi.Extensions.CustomExceptions;
using CSN.WebApi.Models.AccEmployee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountEmployeeController : ControllerBase
    {

        private readonly EFContext eFContext;
        private readonly IAccEmployeeService accEmployeeService;
        private readonly ILogger<AccountEmployeeController> logger;

        public AccountEmployeeController(EFContext eFContext, IAccEmployeeService accEmployeeService, ILogger<AccountEmployeeController> logger)
        {
            this.eFContext = eFContext;
            this.accEmployeeService = accEmployeeService;
            this.logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AccEmployeeLogin request)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Data is not correct");
            }

            var response = await accEmployeeService.LoginAsync(new AccEmployeeLoginRequest()
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
        public async Task<IActionResult> Register([FromBody] AccEmployeeRegister request)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Data is not correct");
            }

            string role = "Employee";

            var response = await accEmployeeService.RegisterAsync(new AccEmployeeRegisterRequest()
            {
                Login = request.Login,
                Email = request.Email,
                Password = request.Password,
                Image = request.Image,
                Role = role,
                CompanyId = request.CompanyId
            });

            return Ok(new
            {
                response.IsSuccess
            });
        }

        [HttpGet("Info"), Authorize(Roles = "Employee")]
        public async Task<IActionResult> Info()
        {
            var response = await accEmployeeService.InfoAsync(new AccEmployeeInfoRequest());

            return Ok(new
            {
                response.Id,
                response.Login,
                response.Email,
                response.Role,
                response.CompanyId,
                response.Company,
                response.Image
            });
        }
    }
}
