using CSN.Application.AppData.Interfaces;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Application.Services.Models.MessageDto;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Adapters
{
    public static class MessageResponseExtension
    {
        public static MessageResponse ToMessageResponse(this Message message)
        {
            return new MessageResponse()
            {
                Id = message.Id,
                Text = message.Text,
                HtmlText = message.HtmlText,
                ModifiedDate = message.ModifiedDate,
                IsRead = message.IsRead,
                AuthorId = message.AuthorId,
                TargetMessageId = message.TargetMessageId,
                ChannelId = message.ChannelId
            };
        }
    }
}