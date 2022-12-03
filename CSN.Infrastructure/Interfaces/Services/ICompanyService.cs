using CSN.Infrastructure.Models.CompanyDto;

namespace CSN.Infrastructure.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<CompanyLoginResponse> LoginAsync(CompanyLoginRequest request);
        Task<CompanyRegisterResponse> RegisterAsync(CompanyRegisterRequest request);
        Task<CompanyConfirmationResponse> ConfirmAccountAsync(CompanyConfirmationRequest request);
        Task<CompanyInfoResponse> GetInfoAsync(CompanyInfoRequest request);
    }
}
