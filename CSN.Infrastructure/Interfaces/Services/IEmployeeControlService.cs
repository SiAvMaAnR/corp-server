using CSN.Infrastructure.Models.CompanyDto;
using CSN.Infrastructure.Models.EmployeeControlDto;

namespace CSN.Infrastructure.Interfaces.Services;

public interface IEmployeeControlService
{
    Task<EmployeeControlChangeRoleResponse> ChangeRoleAsync(EmployeeControlChangeRoleRequest request);
    Task<EmployeeControlEmployeesResponse> GetEmployeesAsync(EmployeeControlEmployeesRequest request);
    Task<EmployeeControlRemoveResponse> RemoveEmployeeAsync(EmployeeControlRemoveRequest request);
}
