using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Persistence.DBContext;
using CSN.WebApi.Models.Employee;
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
        public async Task<IActionResult> AddAsync([FromBody] EmployeeAdd request)
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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<Employee>? employees = await this.employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id:int}"), Authorize]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            Employee? employee = await this.employeeService.GetAsync(id);
            return Ok(employee);
        }
    }
}
