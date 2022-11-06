using CSN.Infrastructure.Models.AccEmployeeDto;

namespace CSN.Infrastructure.Interfaces.Services
{
    public interface IAccEmployeeService
    {
        Task<AccEmployeeLoginResponse> LoginAsync(AccEmployeeLoginRequest request);
        Task<AccEmployeeRegisterResponse> RegisterAsync(AccEmployeeRegisterRequest request);
        Task<AccEmployeeInfoResponse> InfoAsync(AccEmployeeInfoRequest request);
    }
}
