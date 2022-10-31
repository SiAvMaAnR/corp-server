using CSN.Domain.Entities.Companies;
using CSN.Infrastructure.Models.AccCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Infrastructure.Interfaces.Services
{
    public interface IAccCompanyService
    {
        Task<AccCompanyLoginResponse> LoginAsync(AccCompanyLoginRequest request);
        Task<AccCompanyRegisterResponse> RegisterAsync(AccCompanyRegisterRequest request);
        Task<AccCompanyInfoResponse> InfoAsync(AccCompanyInfoRequest request);
    }
}
