using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CSN.Application.Services.Common
{
    public abstract class BaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly ClaimsPrincipal? claimsPrincipal;

        public BaseService(IUnitOfWork unitOfWork, IHttpContextAccessor context)
        {
            this.unitOfWork = unitOfWork;
            this.claimsPrincipal = context.HttpContext?.User;
        }
    }
}
