using System.Security.Claims;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Domain.Entities.Channels;
using CSN.SignalR.Hubs.Common;
using CSN.SignalR.Hubs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CSN.SignalR.Hubs;

public class ChatHub : BaseHub, IHub
{
    private readonly IChannelService channelService;


    public ChatHub(IChannelService channelService)
    {
        this.channelService = channelService;
    }


    [Authorize]
    public override async Task OnConnectedAsync()
    {
        this.channelService.SetClaimsPrincipal(Context?.User);
        await base.OnConnectedAsync();
    }

    [Authorize]
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }


    public async Task GetAllChannelsAsync()
    {
        var result = await this.channelService.GetAllAsync(new ChannelGetAllRequest());
        IList<Channel>? channels = result.Channels;
        await Clients.Caller.SendAsync("Receive", channels);
    }
}