using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.EmployeeControlDto;
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

    [HttpPut("ChangeRole"), Authorize(Policy = "OnlyCompany")]
    public async Task<IActionResult> ChangeRoleEmployee([FromBody] CompanyChangeRole request)
    {
        var response = await employeeControlService.ChangeRoleAsync(new EmployeeControlChangeRoleRequest()
        {
            EmployeeId = request.EmployeeId,
            EmployeePost = request.EmployeePost
        });

        return Ok(new
        {
            response.EmployeeId,
            response.EmployeePost
        });
    }

    [HttpDelete("Remove"), Authorize(Policy = "OnlyCompany")]
    public async Task<IActionResult> RemoveEmployee([FromBody] CompanyRemoveEmployee request)
    {
        var response = await employeeControlService.RemoveEmployeeAsync(new EmployeeControlRemoveRequest(request.Id));

        return Ok(new
        {
            response.IsSuccess
        });
    }

    [HttpGet("GetAll"), Authorize(Policy = "OnlyCompany")]
    public async Task<IActionResult> GetEmployees([FromQuery] CompanyGetEmployees request)
    {
        var response = await employeeControlService.GetEmployeesAsync(new EmployeeControlEmployeesRequest()
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
            response.OnlineCount,
            response.Employees
        });
    }
}
