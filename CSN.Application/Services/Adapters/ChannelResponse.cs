using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.AppData.Interfaces;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Application.Services.Models.MessageDto;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;
using CSN.Persistence.Extensions;

namespace CSN.Application.Services.Adapters
{
    public static class ChannelResponseForAllExtension
    {
        public static IEnumerable<ChannelResponseForAll>? ToChannelResponseForAll(this IList<Channel> channels, User targetUser)
        {
            return channels?.Select(channel => new ChannelResponseForAll()
            {
                Id = channel.Id,
                Name = channel.Name,
                CompanyId = channel.CompanyId,
                IsDeleted = channel.IsDeleted,
                IsDialog = channel.IsDialog,
                IsPublic = channel.IsPublic,
                IsPrivate = channel.IsPrivate,
                UnreadMessagesCount = channel.GetUnreadMessagesCount(targetUser),
                CreatedAt = channel.CreatedAt,
                UpdatedAt = channel.UpdatedAt,
                LastActivity = channel.LastActivity,
                Users = channel.Users.Select(user => new UserResponseForAll()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Login = user.Login,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    Role = user.Role,
                    Image = Convert.ToBase64String(user.Image.ReadToBytes() ?? new byte[0]),
                }).ToList(),
                Messages = channel.Messages.Select(message => new MessageResponseForAll()
                {
                    Id = message.Id,
                    AuthorId = message.AuthorId,
                    Login = message.Author.Login,
                    Text = message.Text,
                })
                .Take(1)
                .ToList()
            });
        }

        public static ChannelResponseForOne ToChannelResponseForOne(this Channel channel, User targetUser, IAppData appData)
        {
            return new ChannelResponseForOne()
            {
                Id = channel.Id,
                Name = channel.Name,
                CompanyId = channel.CompanyId,
                IsDeleted = channel.IsDeleted,
                IsDialog = channel.IsDialog,
                IsPublic = channel.IsPublic,
                IsPrivate = channel.IsPrivate,
                UnreadMessagesCount = channel.GetUnreadMessagesCount(targetUser),
                CreatedAt = channel.CreatedAt,
                UpdatedAt = channel.UpdatedAt,
                LastActivity = channel.LastActivity,
                Users = channel.Users.Select(user => new UserResponseForOne()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Login = user.Login,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    Role = user.Role,
                    Post = (user as Employee)?.Post.ToString(),
                    State = appData.GetById(user.Id)?.State ?? UserState.Offline,
                    Image = Convert.ToBase64String(user.Image.ReadToBytes() ?? new byte[0]),
                }).ToList(),
                Messages = channel.Messages.Select(message => new MessageResponseForOne()
                {
                    Id = message.Id,
                    AuthorId = message.AuthorId,
                    Login = message.Author.Login,
                    Text = message.Text,
                    Html = message.HtmlText,
                    CreatedAt = message.CreatedAt,
                    IsRead = message.IsRead,
                    Attachments = message.Attachments.Select(attachment => new AttachmentResponse()
                    {
                        Id = attachment.Id,
                        Content = Convert.ToBase64String(attachment.Content.ReadToBytes() ?? new byte[0]),
                        ContentType = attachment.ContentType,
                        CreatedAt = attachment.CreatedAt
                    }).ToList()
                }).ToList(),
            };
        }
    }
}
