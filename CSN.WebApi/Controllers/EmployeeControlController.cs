using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.CompanyDto;
using CSN.Infrastructure.Models.EmployeeControlDto;
using CSN.WebApi.Models.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeControlController : ControllerBase
{
    private readonly IEmployeeControlService employeeControlService;
    private readonly ILogger<CompanyController> logger;

    public EmployeeControlController(IEmployeeControlService employeeControlService, ILogger<CompanyController> logger)
    {
        this.employeeControlService = employeeControlService;
        this.logger = logger;
    }

    [HttpPost("ChangeRole"), Authorize(Roles = "Company")]
    public async Task<IActionResult> ChangeRoleEmployee([FromBody] CompanyChangeRole request)
    {
        var response = await this.employeeControlService.ChangeRoleAsync(new EmployeeControlChangeRoleRequest()
        {
            EmployeeId = request.EmployeeId,
            EmployeeRole = request.EmployeeRole
        });

        return Ok(new
        {
            response.EmployeeId,
            response.EmployeeRole
        });
    }

    [HttpPost("Remove"), Authorize(Roles = "Company")]
    public async Task<IActionResult> RemoveEmployee([FromBody] CompanyRemoveEmployee request)
    {
        var response = await this.employeeControlService.RemoveEmployeeAsync(new EmployeeControlRemoveRequest(request.Id));

        return Ok(new
        {
            response.IsSuccess
        });
    }

    [HttpGet("GetAll"), Authorize(Roles = "Company")]
    public async Task<IActionResult> GetEmployees()
    {
        var response = await this.employeeControlService.GetEmployeesAsync(new EmployeeControlEmployeesRequest());

        return Ok(new
        {
            response.Employees.Count,
            response.Employees
        });
    }
}
