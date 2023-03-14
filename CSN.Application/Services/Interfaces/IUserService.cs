using CSN.Application.Services.Models.UserDto;

namespace CSN.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserStateResponse> SetState(UserStateRequest request);
}
