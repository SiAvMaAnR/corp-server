using System.Security.Claims;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Domain.Entities.Channels;
using CSN.SignalR.Hubs.Common;
using CSN.SignalR.Hubs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using static CSN.Application.Services.Filters.ChannelFilters;

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


    [Authorize]
    public async Task GetAllChannelsAsync(int pageNumber, int pageSize, GetAllFilter filter)
    {
        try
        {
            var result = await this.channelService.GetAllAsync(new ChannelGetAllRequest()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Filter = filter
            });

            await Clients.Caller.SendAsync("GetAllChannels", new
            {
                channels = result.Channels,
                channelsCount = result.ChannelsCount,
                pageNumber = result.PageNumber,
                pageSize = result.PageSize,
                pageCount = result.PagesCount,
                unreadChannelsCount = result.UnreadChannelsCount
            });
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("GetAllChannels", null, exception.Message);
        }
    }


    [Authorize]
    public async Task CreateDialogChannelAsync(int targetUserId)
    {
        try
        {
            var result = await this.channelService.CreateAsync(new DialogChannelCreateRequest()
            {
                TargetUserId = targetUserId
            });

            IEnumerable<string> clients = result.Users
                .Where(user => user.ConnectionId != null)
                .Select(user => user.ConnectionId!);

            await Clients.Clients(clients).SendAsync("CreateDialogChannel", result.IsSuccess);
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("CreateDialogChannel", false, exception.Message);
        }
    }

    [Authorize]
    public async Task CreatePublicChannelAsync(string name)
    {
        try
        {
            var result = await this.channelService.CreateAsync(new PublicChannelCreateRequest()
            {
                Name = name
            });

            await Clients.Caller.SendAsync("CreatePublicChannel", result.IsSuccess);
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("CreatePublicChannel", false, exception.Message);
        }
    }

    [Authorize]
    public async Task CreatePrivateChannelAsync(string name)
    {
        try
        {
            var result = await this.channelService.CreateAsync(new PrivateChannelCreateRequest()
            {
                Name = name
            });

            await Clients.Caller.SendAsync("CreatePrivateChannel", result.IsSuccess);
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("CreatePrivateChannel", false, exception.Message);
        }
    }
}