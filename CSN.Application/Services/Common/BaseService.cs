using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CSN.Application.Services.Common
{

    public interface IBaseService
    {
        void SetClaimsPrincipal(ClaimsPrincipal? claimsPrincipal);
    }

    public abstract class BaseService : IBaseService
    {
        protected readonly IUnitOfWork unitOfWork;
        protected ClaimsPrincipal? claimsPrincipal;

        public BaseService(IUnitOfWork unitOfWork, IHttpContextAccessor context)
        {
            this.unitOfWork = unitOfWork;
            this.claimsPrincipal = context.HttpContext?.User;
        }

        public void SetClaimsPrincipal(ClaimsPrincipal? claimsPrincipal)
        {
            this.claimsPrincipal = claimsPrincipal;
        }
    }
}
