
using CSN.Application.Services.Common;
using CSN.Application.Services.Models.EmployeeControlDto;

namespace CSN.Application.Services.Interfaces;

public interface IEmployeeControlService: IBaseService
{
    Task<EmployeeControlChangePostResponse> ChangePostAsync(EmployeeControlChangePostRequest request);
    Task<EmployeeControlEmployeesResponse> GetEmployeesAsync(EmployeeControlEmployeesRequest request);
    Task<EmployeeControlRemoveResponse> RemoveEmployeeAsync(EmployeeControlRemoveRequest request);
}
