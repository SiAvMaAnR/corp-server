using CSN.Infrastructure.Models.CompanyDto;

namespace CSN.Infrastructure.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<CompanyEmployeesResponse> GetEmployeesAsync(CompanyEmployeesRequest request);
        Task<CompanyLoginResponse> LoginAsync(CompanyLoginRequest request);
        Task<CompanyRegisterResponse> RegisterAsync(CompanyRegisterRequest request);
        Task<CompanyInfoResponse> GetInfoAsync(CompanyInfoRequest request);
        Task<CompanyRemoveEmployeeResponse> RemoveEmployeeAsync(CompanyRemoveEmployeeRequest request);
        Task<CompanyConfirmationResponse> ConfirmAccountAsync(CompanyConfirmationRequest request);
    }
}
