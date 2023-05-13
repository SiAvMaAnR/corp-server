

using CSN.Application.AppContext.Models;
using CSN.Application.AppData.Interfaces;
using CSN.Application.Extensions;
using CSN.Application.Services.Adapters;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Application.Services.Models.MessageDto;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;
using CSN.Domain.Exceptions;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CSN.Application.Services;

public class MessageService : BaseService, IMessageService
{
    private readonly IConfiguration configuration;
    private IAppData appData;


    public MessageService(IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration, IAppData appData)
    : base(unitOfWork, context)
    {
        this.configuration = configuration;
        this.appData = appData;
    }

    public async Task<ChannelGetUnreadMessagesResponse> GetUnreadMessagesAsync(ChannelGetUnreadMessagesRequest request)
    {
        var messages = (await this.unitOfWork.Message.GetAllAsync(
            message => message.ChannelId == request.ChannelId,
            message => message.ReadUsers))?.ToList();


        UserC userC = this.appData.GetByChatCId(request.ConnectionId) ??
            throw new NotFoundException("User connection not found");

        User? user = await this.unitOfWork.User.GetAsync(
            (user) => user.Id == userC.Id) ??
            throw new NotFoundException("User not found");

        int unreadMessagesCount = messages?.Count(message => !message.IsContainsReadUser(user)) ?? 0;

        return new ChannelGetUnreadMessagesResponse()
        {
            UnreadMessages = unreadMessagesCount
        };
    }

    public async Task<MessageSendResponse> SendAsync(MessageSendRequest request)
    {
        if (this.claimsPrincipal == null)
            throw new ForbiddenException("Forbidden");

        User? user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
            throw new BadRequestException("Account is not found");

        Channel? channel = await this.unitOfWork.Channel.GetAsync(
            (channel) =>
                channel.IsDeleted == false &&
                channel.Users.Contains(user) &&
                channel.Id == request.ChannelId,
            (channel) => channel.Users,
            (channel) => channel.Messages);

        if (channel == null) throw new BadRequestException("Channel is not found");

        Message message = new Message()
        {
            Text = request.Text,
            HtmlText = request.Html,
            TargetMessageId = request.TargetMessageId,
            Author = user,
            ReadUsers = { user }
        };

        channel.Messages.Add(message);
        channel.LastActivity = DateTime.Now;

        await this.unitOfWork.SaveChangesAsync();
        return new MessageSendResponse()
        {
            Users = channel.Users,
            Message = message.ToMessageResponse(),
            LastActivity = channel.LastActivity,
            ChannelId = channel.Id,
        };
    }
}