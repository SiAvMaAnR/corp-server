
using CSN.Application.Services.Common;
using CSN.Application.Services.Models.EmployeeControlDto;

namespace CSN.Application.Services.Interfaces;

public interface IEmployeeControlService: IBaseService
{
    Task<EmployeeControlChangeRoleResponse> ChangeRoleAsync(EmployeeControlChangeRoleRequest request);
    Task<EmployeeControlEmployeesResponse> GetEmployeesAsync(EmployeeControlEmployeesRequest request);
    Task<EmployeeControlRemoveResponse> RemoveEmployeeAsync(EmployeeControlRemoveRequest request);
}
