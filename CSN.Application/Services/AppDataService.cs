using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using CSN.Infrastructure.Extensions;
using CSN.Application.AppData.Interfaces;
using CSN.Domain.Entities.Users;
using CSN.Infrastructure.Exceptions;
using CSN.Application.Services.Models.AppDataDto;

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

        public async Task<UserStateResponse> SetState(UserStateRequest request)
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

        public async Task<UserAddResponse> AddUser(UserAddRequest request)
        {
            if (this.claimsPrincipal == null)
            {
                throw new BadRequestException("Account is not found");
            }
            var user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork);
            if (user != null) this.appData.Create(user);

            return new UserAddResponse(true);
        }

        public async Task<UserRemoveResponse> RemoveUser(UserRemoveRequest request)
        {
            if (this.claimsPrincipal == null)
            {
                throw new BadRequestException("Account is not found");
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