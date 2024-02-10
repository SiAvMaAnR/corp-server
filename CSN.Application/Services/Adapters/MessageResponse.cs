using System.Net.Http;
using CSN.Application.AppData.Interfaces;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Application.Services.Models.MessageDto;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;
using CSN.Persistence.Extensions;

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
                ChannelId = message.ChannelId,
                CreatedAt = message.CreatedAt,
                Attachments = message.Attachments.Select(attachment => new AttachmentResponse()
                {
                    Id = attachment.Id,
                    Content = Convert.ToBase64String(attachment.Content.ReadToBytes() ?? new byte[0]),
                    ContentType = attachment.ContentType,
                    CreatedAt = attachment.CreatedAt
                }).ToList()
            };
        }
    }
}