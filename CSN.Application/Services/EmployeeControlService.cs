using CSN.Application.AppData.Interfaces;
using CSN.Application.Extensions;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.EmployeeControlDto;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Exceptions;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Domain.Shared.Enums;
using CSN.Persistence.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CSN.Application.Services;

public class EmployeeControlService : BaseService, IEmployeeControlService
{
    private readonly IConfiguration configuration;
    private readonly IAppData appData;

    public EmployeeControlService(IUnitOfWork unitOfWork, IHttpContextAccessor context,
        IConfiguration configuration, IAppData appData)
        : base(unitOfWork, context)
    {
        this.configuration = configuration;
        this.appData = appData;
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
            Post = employee.Post,
            State = this.appData.GetById(employee.Id)?.State ?? UserState.Offline,
            Image = employee.Image.ReadToBytes(),
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt,
        });

        var employeesCount = employeesAll?.ToList().Count ?? 0;
        var employeesOnlineCount = employeesAll?.Count(employee => employee.State == UserState.Online) ?? 0;

        var employees = employeesAll?
            .OrderByDescending(employee => employee.UpdatedAt)
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
            OnlineCount = employeesOnlineCount,
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

    public async Task<EmployeeControlChangePostResponse> ChangePostAsync(EmployeeControlChangePostRequest request)
    {
        var employee = await this.unitOfWork.Employee.GetAsync(employee => employee.Id == request.EmployeeId);

        if (employee == null)
        {
            throw new NotFoundException("Employee is not found");
        }

        employee.Post = request.EmployeePost;

        await this.unitOfWork.Employee.UpdateAsync(employee);

        await this.unitOfWork.SaveChangesAsync();

        return new EmployeeControlChangePostResponse()
        {
            EmployeeId = request.EmployeeId,
            EmployeePost = request.EmployeePost
        };
    }
}
