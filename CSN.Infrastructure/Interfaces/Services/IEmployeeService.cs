using CSN.Infrastructure.Models.EmployeeDto;

namespace CSN.Infrastructure.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeLoginResponse> LoginAsync(EmployeeLoginRequest request);
        Task<EmployeeRegisterResponse> RegisterAsync(EmployeeRegisterRequest request);
        Task<EmployeeInfoResponse> GetInfoAsync(EmployeeInfoRequest request);
        Task<EmployeeRemoveResponse> RemoveAsync(EmployeeRemoveRequest request);
    }
}
