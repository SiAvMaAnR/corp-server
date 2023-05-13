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
using CSN.Application.Services.Models.MessageDto;

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

            var channelsQuery = await this.unitOfWork.Channel.CustomAsync();

            IEnumerable<Channel>? channelsAll = channelsQuery
            .Include(channel => channel.Users)
            .Include(channel => channel.Messages.OrderByDescending(message => message.CreatedAt))
                .ThenInclude(message => message.ReadUsers)
            .Where(channel => !channel.IsDeleted && channel.Users.Contains(user));

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

            Channel? channel = await this.unitOfWork.Channel.GetAsync(
                (channel) =>
                    channel.IsDeleted == false &&
                    channel.Users.Contains(user) &&
                    channel.Id == request.Id,
                (channel) => channel.Messages
                    .OrderByDescending(message => message.CreatedAt)
                    .Take(request.Count),
                (channel) => channel.Users);

            if (channel == null)
                throw new NotFoundException("Channel not found");

            await this.unitOfWork.SaveChangesAsync();

            return new ChannelGetResponse(channel?.ToChannelResponseForOne(user, this.appData));
        }

        public async Task<MessageReadResponse> ReadAsync(MessageReadRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User? user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork, user => user.Channels) ??
                throw new BadRequestException("Account is not found");

            Channel? channel = await (await this.unitOfWork.Channel.CustomAsync())
                .Include(channel => channel.Messages)
                    .ThenInclude(message => message.ReadUsers)
                .Include(channel => channel.Users)
                .FirstOrDefaultAsync(channel => channel.Id == request.ChannelId);

            if (channel == null)
                throw new NotFoundException("Channel not found");

            if(user.GetCompanyId() != channel.CompanyId)
                throw new ForbiddenException("Forbidden");

            foreach (var message in channel.Messages)
            {
                if (message.AuthorId != user.Id)
                {
                    message.IsRead = true;
                }

                if (!message.ReadUsers.Any(readUser => readUser.Id == user.Id))
                    message.ReadUsers.Add(user);
            }

            await this.unitOfWork.SaveChangesAsync();

            var unReadMessages = channel.Messages.Where(message => !message.IsRead);

            var unreadMessagesCount = channel.Messages.Count(message => !message.IsContainsReadUser(user));

            return new MessageReadResponse()
            {
                Users = channel.Users,
                ChannelId = channel.Id,
                UnReadMessageIds = unReadMessages.Select(message => message.Id),
                UnreadMessagesCount = unreadMessagesCount
            };
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
