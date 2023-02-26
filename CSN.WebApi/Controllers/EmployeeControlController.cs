using CSN.Application.Interfaces.Services;
using CSN.Application.Models.EmployeeControlDto;
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

    [HttpPut("ChangeRole"), Authorize(Roles = "Company")]
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

    [HttpDelete("Remove"), Authorize(Roles = "Company")]
    public async Task<IActionResult> RemoveEmployee([FromBody] CompanyRemoveEmployee request)
    {
        var response = await this.employeeControlService.RemoveEmployeeAsync(new EmployeeControlRemoveRequest(request.Id));

        return Ok(new
        {
            response.IsSuccess
        });
    }

    [HttpGet("GetAll"), Authorize(Roles = "Company")]
    public async Task<IActionResult> GetEmployees([FromQuery] CompanyGetEmployees request)
    {
        var response = await this.employeeControlService.GetEmployeesAsync(new EmployeeControlEmployeesRequest()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        });

        return Ok(new
        {
            response.PageSize,
            response.PagesCount,
            response.PageNumber,
            response.EmployeesCount,
            response.Employees
        });
    }
}
