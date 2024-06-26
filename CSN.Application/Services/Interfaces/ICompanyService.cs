
using CSN.Application.Services.Common;
using CSN.Application.Services.Models.CompanyDto;

namespace CSN.Application.Services.Interfaces;

public interface ICompanyService : IBaseService
{
    Task<CompanyLoginResponse> LoginAsync(CompanyLoginRequest request);
    Task<CompanyRegisterResponse> RegisterAsync(CompanyRegisterRequest request);
    Task<CompanyConfirmationResponse> ConfirmAccountAsync(CompanyConfirmationRequest request);
    Task<CompanyInfoResponse> GetInfoAsync(CompanyInfoRequest request);
    Task<CompanyEditResponse> EditAsync(CompanyEditRequest request);
}