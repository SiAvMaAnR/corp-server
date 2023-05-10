using System.Security.Authentication.ExtendedProtection;
using System;
using System.Diagnostics;
using CSN.Application.AppData.Interfaces;
using CSN.Application.Extensions;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Channels.DialogChannel;
using CSN.Domain.Entities.Channels.PrivateChannel;
using CSN.Domain.Entities.Channels.PublicChannel;
using CSN.Domain.Entities.Users;
using CSN.Domain.Exceptions;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using static CSN.Application.Services.Filters.ChannelFilters;
using CSN.Application.Services.Adapters;
using Microsoft.EntityFrameworkCore;

namespace CSN.Application.Services
{
    public class ChannelService : BaseService, IChannelService
    {
        private IAppData appData;

        public ChannelService(IUnitOfWork unitOfWork, IHttpContextAccessor context, IAppData appData)
            : base(unitOfWork, context)
        {
            this.appData = appData;
        }

        public async Task<ChannelGetUsersResponse> GetUsersOfChannelAsync(ChannelGetUsersRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Account is not found");

            Channel? channelWithUsers = await this.unitOfWork.Channel.GetAsync(
                (channel) => channel.CompanyId == companyId,
                (channel) => channel.Users);

            List<User>? users = channelWithUsers?.Users.ToList();

            return new ChannelGetUsersResponse()
            {
                Users = users,
                UsersCount = users?.Count ?? 0
            };
        }

        public async Task<ChannelGetAllOfCompanyResponse> GetAllOfCompanyAsync(ChannelGetAllOfCompanyRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Account is not found");

            IEnumerable<Channel>? channelsAll = await this.unitOfWork.Channel.GetAllAsync((channel) =>
                channel.Users.All(userI => userI.Id != user.Id) &&
                channel.IsDeleted == false &&
                channel.CompanyId == companyId
            );

            string searchFilter = request.SearchFilter?.ToLower() ?? "";

            IEnumerable<Channel>? filterChannels = channelsAll?.Where(channel =>
                channel?.Name?.ToLower().Contains(searchFilter) ?? false ||
                ((channel as DialogChannel)?.GetInterlocutor(user)?.Login?.ToLower()?.Contains(searchFilter) ?? false));

            filterChannels = request.TypeFilter switch
            {
                GetAllFilter.OnlyPublic => filterChannels?.OfType<PublicChannel>(),
                GetAllFilter.OnlyPrivate => filterChannels?.OfType<PrivateChannel>(),
                GetAllFilter.OnlyDialog => filterChannels?.OfType<DialogChannel>(),
                _ => throw new BadRequestException("Unknown filter")
            };

            int channelsCount = filterChannels?.ToList().Count ?? 0;

            List<Channel>? channels = filterChannels?
                .OrderByDescending(channel => channel.CreatedAt)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            int pagesCount = (int)Math.Ceiling(((decimal)channelsCount / request.PageSize));

            return new ChannelGetAllOfCompanyResponse()
            {
                Channels = channels,
                ChannelsCount = channelsCount,
                PagesCount = pagesCount,
                PageSize = request.PageSize,
                PageNumber = request.PageNumber,
            };
        }

        public async Task<ChannelGetAllResponse> GetAllAsync(ChannelGetAllRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            IEnumerable<Channel>? channelsAll = await this.unitOfWork.Channel.GetAllAsync(
                (channel) => !channel.IsDeleted &&
                    channel.Users.Contains(user),
                (channel) => channel.Users,
                (channel) => channel.Messages
                    .OrderByDescending(message => message.CreatedAt));

            int unreadChannelsCount = channelsAll?.Count(channel =>
                channel.Messages.Any((message) => message.ReadUsers.Contains(user))) ?? 0;

            string searchFilter = request.SearchFilter?.ToLower() ?? "";

            IEnumerable<Channel>? filterChannels = channelsAll?.Where(channel =>
                channel.Name?.ToLower().Contains(searchFilter) ?? false
                || ((channel as DialogChannel)?.GetInterlocutor(user)?.Login?.ToLower()?.Contains(searchFilter) ?? false));

            filterChannels = request.TypeFilter switch
            {
                GetAllFilter.OnlyDialog => filterChannels?.OfType<DialogChannel>(),
                GetAllFilter.OnlyPrivate => filterChannels?.OfType<PrivateChannel>(),
                GetAllFilter.OnlyPublic => filterChannels?.OfType<PublicChannel>(),
                _ => filterChannels
            };

            int channelsCount = filterChannels?.ToList()?.Count ?? 0;

            List<Channel>? channels = filterChannels?
                .OrderByDescending(channel => channel.LastActivity)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            int pagesCount = (int)Math.Ceiling(((decimal)channelsCount / request.PageSize));

            var adaptedChannels = channels?.ToChannelResponseForAll(user)?.ToList();

            return new ChannelGetAllResponse()
            {
                Channels = adaptedChannels,
                ChannelsCount = channelsCount,
                UnreadChannelsCount = unreadChannelsCount,
                PagesCount = pagesCount,
                PageSize = request.PageSize,
                PageNumber = request.PageNumber,
            };
        }

        public async Task<ChannelGetResponse> GetAsync(ChannelGetRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User? user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            // Channel? channel1 = await this.unitOfWork.Channel.GetAsync(
            //     (channel) =>
            //         channel.IsDeleted == false &&
            //         channel.Users.Contains(user) &&
            //         channel.Id == request.Id,
            //     (channel) => channel.Messages
            //         .OrderByDescending(message => message.CreatedAt)
            //         .Take(request.Count),
            //     (channel) => channel.Users);

            IQueryable<Channel> queryableChannel = await this.unitOfWork.Channel.CustomAsync();

            Channel? channel = await queryableChannel
                .Include((channel) => channel.Users)
                .Include((channel) => channel.Messages
                    .OrderByDescending(message => message.CreatedAt)
                    .Take(request.Count))
                    .ThenInclude((message) => message.ReadUsers)
                .FirstOrDefaultAsync((channel) =>
                    channel.IsDeleted == false &&
                    channel.Users.Contains(user) &&
                    channel.Id == request.Id);


            if (channel == null)
                throw new NotFoundException("Channel not found");

            foreach (var message in channel.Messages)
            {
                message.IsRead = true;

                if (!message.ReadUsers.Contains(user))
                {
                    message.ReadUsers.Add(user);
                }
            };

            await this.unitOfWork.SaveChangesAsync();

            return new ChannelGetResponse(channel?.ToChannelResponseForOne(user, this.appData));
        }

        public async Task<PublicChannelCreateResponse> CreateAsync(PublicChannelCreateRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            bool isExistsPublicChannel = await this.unitOfWork.PublicChannel.AnyAsync(channel =>
                channel.Name == request.Name && channel.CompanyId == user.GetCompanyId());
            bool isExistsPrivateChannel = await this.unitOfWork.PrivateChannel.AnyAsync(channel =>
                channel.Name == request.Name && channel.CompanyId == user.GetCompanyId());

            if (isExistsPublicChannel || isExistsPrivateChannel)
                throw new BadRequestException("Channel already exists");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            PublicChannel publicChannel = new PublicChannel()
            {
                Name = request.Name,
                AdminId = user.Id,
                Users = { user },
                IsPublic = true,
                CompanyId = companyId
            };

            //================  test
            // var tasks = new List<Task>();
            // for (int i = 0; i < 100; i++)
            // {
            //     PublicChannel publicChannel1 = new PublicChannel()
            //     {
            //         Name = request.Name + DateTime.Now.ToLocalTime() + i,
            //         AdminId = user.Id,
            //         Users = { user },
            //         IsPublic = true,
            //         CompanyId = companyId
            //     };

            //     await this.unitOfWork.PublicChannel.AddAsync(publicChannel1);
            // }
            // await Task.WhenAll(tasks);
            //================

            await this.unitOfWork.PublicChannel.AddAsync(publicChannel);
            await this.unitOfWork.SaveChangesAsync();

            return new PublicChannelCreateResponse(true, publicChannel.Users, publicChannel);
        }

        public async Task<PrivateChannelCreateResponse> CreateAsync(PrivateChannelCreateRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            bool isExistsPublicChannel = await this.unitOfWork.PublicChannel.AnyAsync(channel =>
                channel.Name == request.Name && channel.CompanyId == user.GetCompanyId());
            bool isExistsPrivateChannel = await this.unitOfWork.PrivateChannel.AnyAsync(channel =>
                channel.Name == request.Name && channel.CompanyId == user.GetCompanyId());

            if (isExistsPublicChannel || isExistsPrivateChannel)
                throw new BadRequestException("Channel already exists");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            PrivateChannel privateChannel = new PrivateChannel()
            {
                Name = request.Name,
                AdminId = user.Id,
                Users = { user },
                IsPrivate = true,
                CompanyId = companyId
            };

            await this.unitOfWork.PrivateChannel.AddAsync(privateChannel);
            await this.unitOfWork.SaveChangesAsync();

            return new PrivateChannelCreateResponse(true, privateChannel.Users, privateChannel);
        }

        public async Task<DialogChannelCreateResponse> CreateAsync(DialogChannelCreateRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User? currentUser = await this.claimsPrincipal.GetUserAsync(this.unitOfWork);

            User? targetUser = await this.unitOfWork.User.GetAsync(user => user.Id == request.TargetUserId);

            if (currentUser == null || targetUser == null)
                throw new BadRequestException("Account is not found");

            DialogChannel? existsChannel = await this.unitOfWork.DialogChannel.GetAsync(
                channel =>
                    channel.Users.Contains(currentUser) &&
                    channel.Users.Contains(targetUser),
                channel => channel.Users);

            int companyId = currentUser.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            DialogChannel? dialogChannel;

            if (existsChannel != null)
            {
                dialogChannel = existsChannel;
            }
            else
            {
                DialogChannel newDialogChannel = new DialogChannel()
                {
                    IsDialog = true,
                    CompanyId = companyId,
                    Users = { currentUser, targetUser }
                };

                await this.unitOfWork.DialogChannel.AddAsync(newDialogChannel);
                await this.unitOfWork.SaveChangesAsync();

                dialogChannel = newDialogChannel;
            }

            return new DialogChannelCreateResponse(true, dialogChannel.Users, dialogChannel);
        }

        public async Task<ChannelAddUserResponse> AddUserAsync(ChannelAddUserRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User? currentUser = await this.claimsPrincipal.GetUserAsync(this.unitOfWork);

            User? targetUser = (request.TargetUserId != null)
                ? await this.unitOfWork.User.GetAsync(
                    user => user.Id == request.TargetUserId,
                    user => user.Channels)
                : currentUser;

            if (currentUser == null || targetUser == null)
                throw new BadRequestException("Account is not found");

            Channel? channel = await this.unitOfWork.Channel.GetAsync(
                (channel) => channel.Id == request.ChannelId &&
                    targetUser.GetCompanyId() == channel.CompanyId,
                (channel) => channel.Users);

            if (channel == null)
                throw new ForbiddenException("Channel is not found");

            if (channel is PrivateChannel && currentUser.Id == targetUser.Id)
                throw new ForbiddenException("No access to this channel");

            if (channel is DialogChannel && channel.Users.Count >= 1)
                throw new BadRequestException("The maximum number of users in the dialog");

            bool isExistsUser = channel?.Users.Contains(targetUser) ?? true;

            if (isExistsUser)
                throw new BadRequestException("User already exists");

            channel!.LastActivity = DateTime.Now;
            channel?.Users.Add(targetUser);
            await this.unitOfWork.SaveChangesAsync();

            return new ChannelAddUserResponse(true, channel!.Users, channel);
        }
    }
}
