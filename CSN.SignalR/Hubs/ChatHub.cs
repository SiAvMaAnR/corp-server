using System.Diagnostics;
using System.Security.Claims;
using CSN.Application.Services.Helpers;
using CSN.Application.Services.Helpers.Enums;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.AppDataDto;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Application.Services.Models.MessageDto;
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
    private readonly IMessageService messageService;
    private readonly IAppDataService appDataService;

    public ChatHub(IChannelService channelService, IMessageService messageService, IAppDataService appDataService)
    {
        this.channelService = channelService;
        this.messageService = messageService;
        this.appDataService = appDataService;
    }

    [Authorize]
    public override async Task OnConnectedAsync()
    {
        this.channelService.SetClaimsPrincipal(Context?.User);
        await this.appDataService.ConnectUserAsync(new UserConnectRequest(Context?.ConnectionId, HubType.Chat));
        await base.OnConnectedAsync();
    }

    [Authorize]
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await this.appDataService.DisconnectUserAsync(new UserDisconnectRequest(HubType.Chat));
        await base.OnDisconnectedAsync(exception);
    }

    [Authorize]
    public async Task SendAsync(int channelId, string text, string html, int? targetMessageId)
    {
        try
        {
            var result = await this.messageService.SendAsync(new MessageSendRequest()
            {
                ChannelId = channelId,
                Text = text,
                Html = html,
                TargetMessageId = targetMessageId
            });
            var ids = this.appDataService.GetConnectionIds(result.Users, HubType.Chat);
            await Clients.Clients(ids).SendAsync("Send", new
            {
                message = result.Message,
                lastActivity = result.LastActivity
            });

            // foreach (var id in ids)
            // {
            //     var response = await this.messageService.GetUnreadMessagesAsync(new ChannelGetUnreadMessagesRequest()
            //     {
            //         ChannelId = channelId,
            //         ConnectionId = id
            //     });

            //     await Clients.Client(id).SendAsync("UnreadMessages", new
            //     {
            //         unreadMessagesCount = response.UnreadMessages,
            //         channelId = channelId,
            //     });
            // }
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("Send", null, exception.Message);
        }
    }


    [Authorize]
    public async Task ReadChannelAsync(int id)
    {
        try
        {
            var result = await this.channelService.ReadAsync(new MessageReadRequest(id));

            var ids = this.appDataService.GetConnectionIds(result.Users, HubType.Chat);

            await Clients.Clients(ids).SendAsync("ReadChannel", new
            {
                channelId = result.ChannelId,
                unReadMessageIds = result.UnReadMessageIds
            });
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("ReadChannel", null, exception.Message);
        }
    }


    [Authorize]
    public async Task GetChannelAsync(int id, int count)
    {
        try
        {
            var result = await this.channelService.GetAsync(new ChannelGetRequest(id, count));
            await Clients.Caller.SendAsync("GetChannel", new
            {
                channel = result.Channel
            });
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("GetChannel", null, exception.Message);
        }
    }

    [Authorize]
    public async Task GetUsersOfChannelAsync(int channelId)
    {
        try
        {
            var result = await this.channelService.GetUsersOfChannelAsync(new ChannelGetUsersRequest()
            {
                ChannelId = channelId
            });

            await Clients.Caller.SendAsync("GetUsersOfChannel", new
            {
                users = result.Users,
                usersCount = result.UsersCount,
            });
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("GetUsersOfChannel", null, exception.Message);
        }
    }

    [Authorize]
    public async Task GetAllChannelsAsync(int pageNumber, int pageSize, GetAllFilter typeFilter, string searchFilter)
    {
        try
        {
            var result = await this.channelService.GetAllAsync(new ChannelGetAllRequest()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TypeFilter = typeFilter,
                SearchFilter = searchFilter
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
            var ids = this.appDataService.GetConnectionIds(result.Users, HubType.Chat);
            await Clients.Clients(ids).SendAsync("CreateDialogChannel", new
            {
                channel = result.Channel,
                isSuccess = result.IsSuccess,
                users = result.Users
            });
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("CreateDialogChannel", null, exception.Message);
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

            await Clients.Caller.SendAsync("CreatePublicChannel", new
            {
                channel = result.Channel,
                isSuccess = result.IsSuccess,
                users = result.Users
            });
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("CreatePublicChannel", null, exception.Message);
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

            await Clients.Caller.SendAsync("CreatePrivateChannel", new
            {
                channel = result.Channel,
                isSuccess = result.IsSuccess,
                users = result.Users
            });
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("CreatePrivateChannel", null, exception.Message);
        }
    }

    [Authorize]
    public async Task AddUserAsync(int channelId, int? targetUserId)
    {
        try
        {
            var result = await this.channelService.AddUserAsync(new ChannelAddUserRequest()
            {
                TargetUserId = targetUserId,
                ChannelId = channelId
            });
            string connectionId = Context.ConnectionId;

            var ids = this.appDataService.GetConnectionIds(result.Users, HubType.Chat);
            await Clients.Clients(ids).SendAsync("AddUser", new
            {
                channel = result.Channel,
                isSuccess = result.IsSuccess,
            });
        }
        catch (Exception exception)
        {
            await Clients.Caller.SendAsync("AddUser", null, exception.Message);
        }
    }
}