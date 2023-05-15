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

            if (user != null)
            {
                this.appData.SetState(user.Id, request.State);
            }

            return new UserStateResponse(true);
        }

        public async Task<UserConnectResponse> ConnectUserAsync(UserConnectRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            var user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork);

            if (user != null)
            {
                this.appData.AddUserConnected(user, request.Type, request.ConnectionId);
            }

            return new UserConnectResponse(true);
        }

        public async Task<UserDisconnectResponse> DisconnectUserAsync(UserDisconnectRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User? user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            if (user != null)
            {
                this.appData.RemoveUserConnected(user.Id, request.Type);
            }

            return new UserDisconnectResponse(true);
        }
    }
}