using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.AppData.Interfaces;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;

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
                    Image = user.Image,
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
                    State = appData.GetById(user.Id)?.State ?? UserState.Offline,
                    Image = user.Image,
                }).ToList(),
                Messages = channel.Messages.Select(message => new MessageResponseForOne()
                {
                    Id = message.Id,
                    AuthorId = message.AuthorId,
                    Login = message.Author.Login,
                    Text = message.Text,
                    Html = message.HtmlText,
                    CreatedAt = message.CreatedAt,
                    IsRead = message.IsRead
                }).ToList(),
            };
        }
    }
}
