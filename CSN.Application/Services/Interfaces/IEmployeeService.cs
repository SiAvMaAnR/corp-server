using CSN.Application.Services.Common;
using CSN.Application.Services.Models.EmployeeDto;

namespace CSN.Application.Services.Interfaces;

public interface IEmployeeService : IBaseService
{
    Task<EmployeeLoginResponse> LoginAsync(EmployeeLoginRequest request);
    Task<EmployeeRegisterResponse> RegisterAsync(EmployeeRegisterRequest request);
    Task<EmployeeInfoResponse> GetInfoAsync(EmployeeInfoRequest request);
    Task<EmployeeRemoveResponse> RemoveAsync(EmployeeRemoveRequest request);
    Task<EmployeeEditResponse> EditAsync(EmployeeEditRequest request);
}
