using CSN.Application.Services.Common;
using CSN.Application.Services.Models.AppDataDto;

namespace CSN.Application.Services.Interfaces
{
    public interface IAppDataService : IBaseService
    {
        Task<UserStateResponse> SetState(UserStateRequest request);

        Task<UserAddResponse> AddUser(UserAddRequest request);

        Task<UserRemoveResponse> RemoveUser(UserRemoveRequest request);
    }
}