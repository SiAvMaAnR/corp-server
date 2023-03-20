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

namespace CSN.Application.Services
{
    public class ChannelService : BaseService, IChannelService
    {
        public ChannelService(IUnitOfWork unitOfWork, IHttpContextAccessor context)
            : base(unitOfWork, context)
        {

        }

        public async Task<ChannelGetAllResponse> GetAllAsync(ChannelGetAllRequest request)
        {
            if (this.claimsPrincipal == null)
            {
                throw new ForbiddenException("Forbidden");
            }

            User? user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork, (user) => user.Channels);

            if (user == null)
            {
                throw new BadRequestException("Account is not found");
            }

            IEnumerable<Channel>? channels = await this.unitOfWork.Channel.GetAllAsync(
                (channel) =>
                    channel.IsDeleted == false &&
                    channel.Users.Any(u => user.Id == u.Id),
                (channel) => channel.Messages,
                (channel) => channel.Users);

            return new ChannelGetAllResponse(channels?.ToList());
        }

        public async Task<ChannelGetResponse> GetAsync(ChannelGetRequest request)
        {
            if (this.claimsPrincipal == null)
            {
                throw new ForbiddenException("Forbidden");
            }

            User? user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork, (user) => user.Channels);

            if (user == null)
            {
                throw new BadRequestException("Account is not found");
            }

            Channel? channel = await this.unitOfWork.Channel.GetAsync(
                (channel) =>
                    channel.IsDeleted == false &&
                    channel.Users.Any(u => user.Id == u.Id) &&
                    channel.Id == request.Id,
                (channel) => channel.Messages,
                (channel) => channel.Users);


            return new ChannelGetResponse(channel);
        }


        public async Task<PublicChannelCreateResponse> CreateAsync(PublicChannelCreateRequest request)
        {
            if (this.claimsPrincipal == null)
            {
                throw new ForbiddenException("Forbidden");
            }

            User? user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork);

            if (user == null)
            {
                throw new BadRequestException("Account is not found");
            }

            bool isExistsChannel = await this.unitOfWork.PublicChannel.AnyAsync(channel => channel.Name == request.Name);

            if (isExistsChannel)
            {
                throw new BadRequestException("Channel already exists");
            }

            var publicChannel = new PublicChannel()
            {
                Name = request.Name,
                Admin = user
            };

            publicChannel.Users.Add(user);
            await this.unitOfWork.PublicChannel.AddAsync(publicChannel);
            await this.unitOfWork.SaveChangesAsync();

            return new PublicChannelCreateResponse(true);
        }

        public async Task<PrivateChannelCreateResponse> CreateAsync(PrivateChannelCreateRequest request)
        {
            if (this.claimsPrincipal == null)
            {
                throw new ForbiddenException("Forbidden");
            }

            User? user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork);

            if (user == null)
            {
                throw new BadRequestException("Account is not found");
            }

            bool isExistsChannel = await this.unitOfWork.PrivateChannel.AnyAsync(channel => channel.Name == request.Name);

            if (isExistsChannel)
            {
                throw new BadRequestException("Channel already exists");
            }

            var privateChannel = new PrivateChannel()
            {
                Name = request.Name,
                Admin = user
            };

            privateChannel.Users.Add(user);
            await this.unitOfWork.PrivateChannel.AddAsync(privateChannel);
            await this.unitOfWork.SaveChangesAsync();

            return new PrivateChannelCreateResponse(true);
        }

        public async Task<DialogChannelCreateResponse> CreateAsync(DialogChannelCreateRequest request)
        {
            if (this.claimsPrincipal == null)
            {
                throw new ForbiddenException("Forbidden");
            }

            User? user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork);

            User? targetUser = await this.unitOfWork.User.GetAsync(user => user.Id == request.TargetUserId);

            if (user == null || targetUser == null)
            {
                throw new BadRequestException("Account is not found");
            }

            bool isExistsChannel = await this.unitOfWork.DialogChannel.AnyAsync(channel =>
                (channel.User1.Id == user.Id && channel.User2.Id == targetUser.Id) ||
                (channel.User1.Id == targetUser.Id && channel.User2.Id == user.Id));

            if (isExistsChannel)
            {
                throw new BadRequestException("Channel already exists");
            }

            var dialogChannel = new DialogChannel()
            {
                User1 = user,
                User2 = targetUser
            };

            dialogChannel.Users.Add(user);
            await this.unitOfWork.DialogChannel.AddAsync(dialogChannel);
            await this.unitOfWork.SaveChangesAsync();

            return new DialogChannelCreateResponse(true);
        }

        public async Task<ChannelAddUserResponse> AddUserAsync(ChannelAddUserRequest request)
        {
            if (this.claimsPrincipal == null)
            {
                throw new ForbiddenException("Forbidden");
            }

            User? user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork);

            User? targetUser = await this.unitOfWork.User.GetAsync(
                user => user.Id == request.ChannelId, 
                user => user.Channels);

            if (user == null || targetUser == null)
            {
                throw new BadRequestException("Account is not found");
            }

            Channel? channel = user.Channels.FirstOrDefault(channel => channel.Id == request.ChannelId);

            bool isExistsUser = channel?.Users.Contains(targetUser) ?? true;
            
            if (isExistsUser)
            {
                throw new BadRequestException("User already exists");
            }

            channel?.Users.Add(targetUser);
            await this.unitOfWork.SaveChangesAsync();

            return new ChannelAddUserResponse(true);
        }
    }
}
