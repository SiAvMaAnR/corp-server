using CSN.Application.Extensions;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.UserDto;
using CSN.Domain.Entities.Users;
using CSN.Domain.Exceptions;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Persistence.Extensions;
using Microsoft.AspNetCore.Http;

namespace CSN.Application.Services;

public class UserService : BaseService, IUserService
{
    public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork, context)
    {
    }

    public async Task<UserGetAllOfCompanyResponse> GetAllOfCompanyAsync(UserGetAllOfCompanyRequest request)
    {
        if (this.claimsPrincipal == null)
            throw new ForbiddenException("Forbidden");

        User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
            throw new BadRequestException("Account is not found");

        int companyId = user.GetCompanyId() ??
            throw new BadRequestException("Account is not found");

        var usersAll = (await this.unitOfWork.User.GetAllAsync(curUser => curUser.Id != user.Id))?
            .AsEnumerable()
            .Where(user => user.GetCompanyId() == companyId);


        string searchFilter = request.SearchFilter?.ToLower() ?? "";

        IEnumerable<User>? filterUsers = usersAll?.Where(user =>
            user?.Login?.ToLower().Contains(searchFilter) ?? false);

        int usersCount = filterUsers?.ToList().Count ?? 0;

        List<UserResponse>? users = filterUsers?
            .OrderByDescending(user => user.Login)
            .Select(user => new UserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Role = user.Role,
                Image = user.Image?.ReadToBytes(),
                CreatedAt = user.CreatedAt
            })
            .ToList();

        return new UserGetAllOfCompanyResponse()
        {
            Users = users,
            UsersCount = usersCount,
        };
    }
}
