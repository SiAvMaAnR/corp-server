using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.UserDto;
using CSN.Domain.Shared.Enums;
using Microsoft.AspNetCore.SignalR;

namespace CSN.SignalR.Hubs;

public class StateHub : Hub
{
    private readonly IUserService userService;

    public StateHub(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task SetState(UserState state)
    {
        await this.userService.SetState(new UserStateRequest(state));
        await this.Clients.Caller.SendAsync("State", true);
    }

    public override async Task OnConnectedAsync()
    {
        await this.userService.SetState(new UserStateRequest(UserState.Online));
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await this.userService.SetState(new UserStateRequest(UserState.Offline));
        await base.OnDisconnectedAsync(exception);
    }
}
