using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Persistence.DBContext;
using CSN.WebApi.DTOs.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EFContext eFContext;
        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(EFContext eFContext, IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            this.eFContext = eFContext;
            this.employeeService = employeeService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] EmployeeAddRequest request)
        {
            try
            {
                await this.employeeService.AddAsync(new Employee()
                {
                    Login = request.Login,
                    Email = request.Email,
                    PasswordHash = new byte[10],
                    PasswordSalt = new byte[10],
                    Role = request.Role,
                    Image = new byte[10],
                    CompanyId = request.CompanyId
                });
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                IEnumerable<Employee>? employees = await this.employeeService.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}"), Authorize]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            try
            {
                Employee? employee = await this.employeeService.GetAsync(id);
                return Ok(employee);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
