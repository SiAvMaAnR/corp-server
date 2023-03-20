using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace CSN.Application.Services;

public class UserService : BaseService, IUserService
{
    public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork, context)
    {
    }

    
}
