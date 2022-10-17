using CSN.Domain.Entities.Employees;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Persistence.DBContext;
using CSN.WebApi.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Services
{
    public class AccEmployeeService : BaseService<Employee>, IAccEmployeeService
    {
        public AccEmployeeService(EFContext eFContext, IUnitOfWork unitOfWork) 
            : base(eFContext, unitOfWork)
        {
        }
    }
}
