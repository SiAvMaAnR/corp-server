using CSN.Application.Interfaces.Services;
using CSN.Application.Models.EmployeeControlDto;
using CSN.Application.Services.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Domain.Shared.Exceptions;
using CSN.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CSN.Application.Services;

public class EmployeeControlService : BaseService<Company>, IEmployeeControlService
{
    private readonly IConfiguration configuration;

    public EmployeeControlService(IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration)
        : base(unitOfWork, context)
    {
        this.configuration = configuration;
    }

    public async Task<EmployeeControlEmployeesResponse> GetEmployeesAsync(EmployeeControlEmployeesRequest request)
    {
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(unitOfWork, company => company.Employees);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        var employeesAll = company.Employees.Select(employee => new CompanyEmployee()
        {
            Id = employee.Id,
            Login = employee.Login,
            Email = employee.Email,
            Role = employee.Role,
            Image = employee.Image,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt,
        });

        var employeesCount = employeesAll?.ToList().Count ?? 0;

        var employees = employeesAll?
            .Skip(request.PageNumber * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var pagesCount = (int)Math.Ceiling(((decimal)employeesCount / request.PageSize));

        return new EmployeeControlEmployeesResponse()
        {
            Employees = employees,
            PageSize = request.PageSize,
            PageNumber = request.PageNumber,
            EmployeesCount = employeesCount,
            PagesCount = pagesCount,
        };
    }

    public async Task<EmployeeControlRemoveResponse> RemoveEmployeeAsync(EmployeeControlRemoveRequest request)
    {
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(unitOfWork);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        Employee? employee = await this.unitOfWork.Employee.GetAsync(employee =>
            employee.Id == request.Id && company.Id == employee.CompanyId);

        if (employee == null)
        {
            throw new NotFoundException("Employee is not found");
        }

        await this.unitOfWork.Employee.DeleteAsync(employee);
        await this.unitOfWork.SaveChangesAsync();

        return new EmployeeControlRemoveResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<EmployeeControlChangeRoleResponse> ChangeRoleAsync(EmployeeControlChangeRoleRequest request)
    {
        var employee = await this.unitOfWork.Employee.GetAsync(employee => employee.Id == request.EmployeeId);

        if (employee == null)
        {
            throw new NotFoundException("Employee is not found");
        }

        employee.Role = request.EmployeeRole.ToString();

        await this.unitOfWork.Employee.UpdateAsync(employee);

        await this.unitOfWork.SaveChangesAsync();

        return new EmployeeControlChangeRoleResponse()
        {
            EmployeeId = request.EmployeeId,
            EmployeeRole = request.EmployeeRole.ToString()
        };
    }
}
