using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.UserDto;
using CSN.Domain.Entities.Users;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Exceptions;
using CSN.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace CSN.Application.Services;

public class UserService : BaseService<User>, IUserService
{
    public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork, context)
    {
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
}
