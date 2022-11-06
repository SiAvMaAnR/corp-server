using CSN.Infrastructure.Models.AccCompany;

namespace CSN.Infrastructure.Interfaces.Services
{
    public interface IAccCompanyService
    {
        Task<AccCompanyLoginResponse> LoginAsync(AccCompanyLoginRequest request);
        Task<AccCompanyRegisterResponse> RegisterAsync(AccCompanyRegisterRequest request);
        Task<AccCompanyInfoResponse> InfoAsync(AccCompanyInfoRequest request);
    }
}
