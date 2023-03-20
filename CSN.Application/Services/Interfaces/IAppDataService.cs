using CSN.Application.Services.Common;
using CSN.Application.Services.Models.AppDataDto;

namespace CSN.Application.Services.Interfaces
{
    public interface IAppDataService : IBaseService
    {
        Task<UserStateResponse> SetStateAsync(UserStateRequest request);
        Task<UserAddResponse> AddUserAsync(UserAddRequest request);
        Task<UserRemoveResponse> RemoveUserAsync(UserRemoveRequest request);
    }
}