using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSN.Infrastructure.Models.AccEmployee;

namespace CSN.Infrastructure.Interfaces.Services
{
    public interface IAccEmployeeService
    {
        Task<AccEmployeeLoginResponse> LoginAsync(AccEmployeeLoginRequest request);
        Task<AccEmployeeRegisterResponse> RegisterAsync(AccEmployeeRegisterRequest request);
        Task<AccEmployeeInfoResponse> InfoAsync(AccEmployeeInfoRequest request);
    }
}
