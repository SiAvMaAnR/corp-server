using CSN.Application.Services.Common;
using CSN.Application.Services.Helpers.Enums;
using CSN.Application.Services.Models.AppDataDto;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Interfaces
{
    public interface IAppDataService : IBaseService
    {
        Task<UserStateResponse> SetStateAsync(UserStateRequest request);
        Task<UserConnectResponse> ConnectUserAsync(UserConnectRequest request);
        Task<UserDisconnectResponse> DisconnectUserAsync(UserDisconnectRequest request);
        IReadOnlyList<string> GetConnectionIds(ICollection<User>? users, HubType type);
    }
}