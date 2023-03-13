
using CSN.Application.Services.Models.EmployeeControlDto;

namespace CSN.Application.Services.Interfaces;

public interface IEmployeeControlService
{
    Task<EmployeeControlChangeRoleResponse> ChangeRoleAsync(EmployeeControlChangeRoleRequest request);
    Task<EmployeeControlEmployeesResponse> GetEmployeesAsync(EmployeeControlEmployeesRequest request);
    Task<EmployeeControlRemoveResponse> RemoveEmployeeAsync(EmployeeControlRemoveRequest request);
}
