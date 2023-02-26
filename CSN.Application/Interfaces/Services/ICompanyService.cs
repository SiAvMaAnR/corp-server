using CSN.Application.Models.CompanyDto;

namespace CSN.Application.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<CompanyLoginResponse> LoginAsync(CompanyLoginRequest request);
        Task<CompanyRegisterResponse> RegisterAsync(CompanyRegisterRequest request);
        Task<CompanyConfirmationResponse> ConfirmAccountAsync(CompanyConfirmationRequest request);
        Task<CompanyInfoResponse> GetInfoAsync(CompanyInfoRequest request);
        Task<CompanyEditResponse> EditAsync(CompanyEditRequest request);
    }
}
