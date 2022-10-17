using CSN.Domain.Entities.Companies;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Persistence.DBContext;
using CSN.WebApi.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Services
{
    public class AccCompanyService : BaseService<Company>
    {
        public AccCompanyService(EFContext eFContext, IUnitOfWork unitOfWork) : base(eFContext, unitOfWork)
        {
        }
    }
}
