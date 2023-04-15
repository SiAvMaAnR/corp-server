using CSN.Application.Extensions;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Channels.DialogChannel;
using CSN.Domain.Entities.Channels.PrivateChannel;
using CSN.Domain.Entities.Channels.PublicChannel;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Users;
using CSN.Domain.Exceptions;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using static CSN.Application.Services.Filters.ChannelFilters;

namespace CSN.Application.Services
{
    public class ChannelService : BaseService, IChannelService
    {
        public ChannelService(IUnitOfWork unitOfWork, IHttpContextAccessor context)
            : base(unitOfWork, context)
        {

        }

        public async Task<ChannelGetAllOfCompanyResponse> GetAllOfCompanyAsync(ChannelGetAllOfCompanyRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.CompanyId ??
                throw new BadRequestException("Account is not found");

            IEnumerable<Channel>? channelsAll = await this.unitOfWork.Channel.GetAllAsync((channel) =>
                channel.IsDeleted == false &&
                channel.CompanyId == companyId
            );

            int channelsCount = channelsAll?.ToList().Count ?? 0;

            string searchFilter = request.SearchFilter?.ToLower() ?? "";

            IEnumerable<Channel>? filterChannels = channelsAll?.Where(channel =>
                ((channel as PublicChannel)?.Name?.ToLower().Contains(searchFilter) ?? false) ||
                ((channel as DialogChannel)?.GetInterlocutor(user)?.Login?.ToLower()?.Contains(searchFilter) ?? false));

            filterChannels = request.TypeFilter switch
            {
                GetAllFilter.OnlyDialog => filterChannels?.OfType<DialogChannel>(),
                GetAllFilter.OnlyPublic => filterChannels?.OfType<PublicChannel>(),
                _ => throw new BadRequestException("Unknown filter")
            };

            List<Channel>? channels = filterChannels?
                .OrderByDescending(channel => channel.CreatedAt)
                .ToList();

            return new ChannelGetAllOfCompanyResponse()
            {
                Channels = channels,
                ChannelsCount = channelsCount,
            };
        }

        public async Task<ChannelGetAllResponse> GetAllAsync(ChannelGetAllRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            IEnumerable<Channel>? channelsAll = await this.unitOfWork.Channel.GetAllAsync(
                (channel) => channel.IsDeleted == false &&
                    channel.Users.Contains(user),
                (channel) => channel.Users,
                (channel) => channel.Messages
                    .OrderByDescending(message => message.CreatedAt)
                    .Take(1));

            int channelsCount = channelsAll?.ToList().Count ?? 0;

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

            List<Channel>? channels = filterChannels?
                .OrderByDescending(channel => channel.LastActivity)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            channels?.ForEach(channel => channel.UpdateUnreadMessagesCount(user));

            int pagesCount = (int)Math.Ceiling(((decimal)channelsCount / request.PageSize));

            return new ChannelGetAllResponse()
            {
                Channels = channels,
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
                (channel) => channel.Messages.OrderByDescending(message => message.CreatedAt),
                (channel) => channel.Users);

            return new ChannelGetResponse(channel);
        }

        public async Task<PublicChannelCreateResponse> CreateAsync(PublicChannelCreateRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            bool isExistsPublicChannel = await this.unitOfWork.PublicChannel.AnyAsync(channel => channel.Name == request.Name);
            bool isExistsPrivateChannel = await this.unitOfWork.PrivateChannel.AnyAsync(channel => channel.Name == request.Name);

            if (isExistsPublicChannel || isExistsPrivateChannel)
                throw new BadRequestException("Channel already exists");

            int companyId = user.CompanyId ??
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

            bool isExistsPublicChannel = await this.unitOfWork.PublicChannel.AnyAsync(channel => channel.Name == request.Name);
            bool isExistsPrivateChannel = await this.unitOfWork.PrivateChannel.AnyAsync(channel => channel.Name == request.Name);

            if (isExistsPublicChannel || isExistsPrivateChannel)
                throw new BadRequestException("Channel already exists");

            int companyId = user.CompanyId ??
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

            bool isExistsChannel = await this.unitOfWork.DialogChannel.AnyAsync(channel =>
                channel.Users.Contains(currentUser) &&
                channel.Users.Contains(targetUser));

            if (isExistsChannel)
                throw new BadRequestException("Channel already exists");

            int companyId = currentUser.CompanyId ??
                throw new BadRequestException("Company not found");

            DialogChannel dialogChannel = new DialogChannel()
            {
                IsDialog = true,
                CompanyId = companyId
            };

            currentUser.Channels.Add(dialogChannel);
            targetUser.Channels.Add(dialogChannel);

            await this.unitOfWork.DialogChannel.AddAsync(dialogChannel);
            await this.unitOfWork.SaveChangesAsync();

            return new DialogChannelCreateResponse(true, dialogChannel.Users, dialogChannel);
        }

        public async Task<ChannelAddUserResponse> AddUserAsync(ChannelAddUserRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User? currentUser = await this.claimsPrincipal.GetUserAsync(this.unitOfWork);

            User? targetUser = await this.unitOfWork.User.GetAsync(
                user => user.Id == request.TargetUserId,
                user => user.Channels);

            if (currentUser == null || targetUser == null)
                throw new BadRequestException("Account is not found");

            Channel? channel = currentUser.Channels.FirstOrDefault(channel => channel.Id == request.ChannelId);

            if (channel is PrivateChannel && currentUser.Id == targetUser.Id)
                throw new ForbiddenException("No access to this channel");

            if (channel is DialogChannel && channel.Users.Count >= 1)
                throw new BadRequestException("The maximum number of users in the dialog");

            bool isExistsUser = channel?.Users.Contains(targetUser) ?? true;

            if (isExistsUser)
                throw new BadRequestException("User already exists");

            channel?.Users.Add(targetUser);
            await this.unitOfWork.SaveChangesAsync();

            return new ChannelAddUserResponse(true);
        }
    }
}
