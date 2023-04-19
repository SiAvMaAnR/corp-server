using CSN.Application.Services.Common;
using CSN.Application.Services.Models.UserDto;

namespace CSN.Application.Services.Interfaces;

public interface IUserService : IBaseService
{
    Task<UserGetAllOfCompanyResponse> GetAllOfCompanyAsync(UserGetAllOfCompanyRequest request);
}
