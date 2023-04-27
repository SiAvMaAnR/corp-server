using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.EmployeeDto;
using CSN.WebApi.Controllers.Models.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            this.employeeService = employeeService;
            this.logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] EmployeeLogin request)
        {
            var response = await this.employeeService.LoginAsync(new EmployeeLoginRequest()
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
        public async Task<IActionResult> Register([FromBody] EmployeeRegister request)
        {
            var registerResponse = await this.employeeService.RegisterAsync(new EmployeeRegisterRequest()
            {
                Login = request.Login,
                Invite = request.Invite,
                Password = request.Password,
                Image = request.Image,
            });

            var loginResponse = await this.employeeService.LoginAsync(new EmployeeLoginRequest()
            {
                Email = registerResponse.Email,
                Password = registerResponse.Password
            });

            return Ok(new
            {
                loginResponse.IsSuccess,
                loginResponse.Token,
                loginResponse.TokenType
            });
        }

        [HttpPut("Edit"), Authorize(Policy = "OnlyEmployee")]
        public async Task<IActionResult> Edit([FromBody] EmployeeEdit request)
        {
            var response = await this.employeeService.EditAsync(new EmployeeEditRequest()
            {
                Login = request.Login,
                Image = request.Image,
            });

            return Ok(new
            {
                response.IsSuccess
            });
        }

        [HttpGet("Info"), Authorize(Policy = "OnlyEmployee")]
        public async Task<IActionResult> Info()
        {
            var response = await this.employeeService.GetInfoAsync(new EmployeeInfoRequest());

            return Ok(new
            {
                response.Id,
                response.Login,
                response.Email,
                response.Role,
                response.CompanyId,
                response.Company,
                response.Image,
                response.State,
                response.CreatedAt,
                response.UpdatedAt
            });
        }

        [HttpDelete("Remove"), Authorize(Policy = "OnlyEmployee")]
        public async Task<IActionResult> Remove()
        {
            var response = await this.employeeService.RemoveAsync(new EmployeeRemoveRequest());

            return Ok(new
            {
                response.IsSuccess
            });
        }
    }
}
