using CSN.Application.Services.Helpers.Enums;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.AppDataDto;
using CSN.Domain.Shared.Enums;
using CSN.SignalR.Hubs.Common;
using CSN.SignalR.Hubs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CSN.SignalR.Hubs;

public class StateHub : BaseHub, IHub
{
    private readonly IAppDataService appDataService;
    public StateHub(IAppDataService appDataService)
    {
        this.appDataService = appDataService;
    }

    [Authorize]
    public async Task SetState(UserState state)
    {
        await this.appDataService.SetStateAsync(new UserStateRequest(state));
        await this.Clients.Caller.SendAsync("State", true);
    }

    public override async Task OnConnectedAsync()
    {
        this.appDataService.SetClaimsPrincipal(Context?.User);

        await this.appDataService.ConnectUserAsync(new UserConnectRequest(Context?.ConnectionId, HubType.State));
        await this.appDataService.SetStateAsync(new UserStateRequest(UserState.Online));
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await this.appDataService.DisconnectUserAsync(new UserDisconnectRequest(HubType.State));
        await this.appDataService.SetStateAsync(new UserStateRequest(UserState.Offline));
        await base.OnDisconnectedAsync(exception);
    }
}
