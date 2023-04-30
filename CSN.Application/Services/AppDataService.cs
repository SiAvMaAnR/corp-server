using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using CSN.Application.AppData.Interfaces;
using CSN.Domain.Entities.Users;
using CSN.Application.Services.Models.AppDataDto;
using CSN.Domain.Exceptions;
using CSN.Application.Extensions;
using CSN.Application.Services.Helpers.Enums;
using CSN.Application.AppContext.Models;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services
{
    public class AppDataService : BaseService, IAppDataService
    {
        private readonly IAppData appData;
        private readonly ReaderWriterLockSlim _lock = new ();

        public AppDataService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor context,
            IAppData appData
        ) : base(unitOfWork, context)
        {
            this.appData = appData;
        }

        public IReadOnlyList<string> GetConnectionIds(ICollection<User>? users, HubType type)
        {
            return this.appData.GetConnectionIds(users, type);
        }

        public async Task<UserStateResponse> SetStateAsync(UserStateRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User? user = await this.claimsPrincipal.GetUserAsync(unitOfWork) ??
                throw new NotFoundException("Account is not found");

            lock (_lock)
            {
                if (user != null)
                {
                    var userC = this.appData.GetById(user.Id) ??
                        throw new BadRequestException("Connected user not found");

                    userC.State = request.State;
                }
            }

            return new UserStateResponse(true);
        }

        public async Task<UserConnectResponse> ConnectUserAsync(UserConnectRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            var user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork);

            lock (_lock)
            {
                if (user != null)
                {
                    var userC = this.appData.GetById(user.Id);

                    if (userC == null)
                    {
                        userC = new UserC(user.Id);
                        this.appData.AddUserConnected(userC);
                    }

                    if (request.Type == HubType.Chat)
                        userC.ChatHubId = request.ConnectionId;
                    else if (request.Type == HubType.State)
                        userC.StateHubId = request.ConnectionId;
                    else if (request.Type == HubType.Notification)
                        userC.NotificationHubId = request.ConnectionId;
                }
            }

            return new UserConnectResponse(true);
        }

        public async Task<UserDisconnectResponse> DisconnectUserAsync(UserDisconnectRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User? user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            lock (_lock)
            {
                if (user != null)
                {
                    var userC = this.appData.GetById(user.Id) ??
                        throw new BadRequestException("Connected user not found");

                    if (request.Type == HubType.Chat)
                        userC.ChatHubId = null;
                    else if (request.Type == HubType.State)
                        userC.StateHubId = null;
                    else if (request.Type == HubType.Notification)
                        userC.NotificationHubId = null;

                    if (userC.ChatHubId == null && userC.StateHubId == null && userC.NotificationHubId == null && userC.State == UserState.Offline)
                    {
                        this.appData.RemoveUserConnected(userC.Id);
                    }
                }
            }

            return new UserDisconnectResponse(true);
        }
    }
}