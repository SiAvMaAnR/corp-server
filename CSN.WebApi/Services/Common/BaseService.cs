using CSN.Domain.Entities.Common;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories;
using CSN.Persistence.UnitOfWork;

namespace CSN.WebApi.Services.Common
{
    public abstract class BaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly EFContext eFContext;

        public BaseService(EFContext eFContext, IUnitOfWork unitOfWork)
        {
            this.eFContext = eFContext;
            this.unitOfWork = unitOfWork;
        }
    }
}
