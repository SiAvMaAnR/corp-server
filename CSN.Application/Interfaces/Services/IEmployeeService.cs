using CSN.Application.Models.EmployeeDto;

namespace CSN.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeLoginResponse> LoginAsync(EmployeeLoginRequest request);
        Task<EmployeeRegisterResponse> RegisterAsync(EmployeeRegisterRequest request);
        Task<EmployeeInfoResponse> GetInfoAsync(EmployeeInfoRequest request);
        Task<EmployeeRemoveResponse> RemoveAsync(EmployeeRemoveRequest request);
        Task<EmployeeEditResponse> EditAsync(EmployeeEditRequest request);
    }
}
