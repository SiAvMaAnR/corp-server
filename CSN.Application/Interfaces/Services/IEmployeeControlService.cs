using CSN.Application.Models.CompanyDto;
using CSN.Application.Models.EmployeeControlDto;

namespace CSN.Application.Interfaces.Services;

public interface IEmployeeControlService
{
    Task<EmployeeControlChangeRoleResponse> ChangeRoleAsync(EmployeeControlChangeRoleRequest request);
    Task<EmployeeControlEmployeesResponse> GetEmployeesAsync(EmployeeControlEmployeesRequest request);
    Task<EmployeeControlRemoveResponse> RemoveEmployeeAsync(EmployeeControlRemoveRequest request);
}
