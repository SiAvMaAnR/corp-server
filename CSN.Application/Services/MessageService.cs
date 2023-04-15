

using CSN.Application.Extensions;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.MessageDto;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;
using CSN.Domain.Exceptions;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CSN.Application.Services;

public class MessageService : BaseService, IMessageService
{
    public readonly IConfiguration configuration;

    public MessageService(IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration)
    : base(unitOfWork, context)
    {
        this.configuration = configuration;
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
            (channel) => channel.Users);

        if (channel == null) throw new BadRequestException("Channel is not found");

        Message message = new Message()
        {
            Text = request.Message,
            TargetMessageId = request.TargetMessageId,
            Author = user
        };

        channel.Messages.Add(message);

        await this.unitOfWork.SaveChangesAsync();

        return new MessageSendResponse()
        {
            Users = channel.Users,
            Message = message
        };
    }
}