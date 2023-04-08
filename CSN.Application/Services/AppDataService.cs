using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using CSN.Application.AppData.Interfaces;
using CSN.Domain.Entities.Users;
using CSN.Application.Services.Models.AppDataDto;
using CSN.Domain.Exceptions;
using CSN.Application.Extensions;

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

        public async Task<UserStateResponse> SetStateAsync(UserStateRequest request)
        {
            User? user = await this.claimsPrincipal!.GetUserAsync(unitOfWork);

            if (user == null)
            {
                throw new NotFoundException("Account is not found");
            }

            user.State = request.State;


            await this.unitOfWork.User.UpdateAsync(user);
            await this.unitOfWork.SaveChangesAsync();

            return new UserStateResponse() { IsSuccess = true };
        }

        public async Task<UserAddResponse> AddUserAsync(UserAddRequest request)
        {
            if (this.claimsPrincipal == null)
            {
                throw new ForbiddenException("Forbidden");
            }
            var user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork);
            if (user != null)
            {
                user.ConnectionId = request.ConnectionId;
                this.appData.Create(user);
            }

            return new UserAddResponse(true);
        }

        public async Task<UserRemoveResponse> RemoveUserAsync(UserRemoveRequest request)
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

            return new UserRemoveResponse()
            {
                IsSuccess = this.appData.Remove(user)
            };
        }
    }
}