using CSN.Domain.Entities.Employees;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Persistence.DBContext;
using CSN.WebApi.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Services
{
    public class AccEmployeeService : BaseService<Employee>
    {
        public AccEmployeeService(EFContext eFContext, IUnitOfWork unitOfWork) : base(eFContext, unitOfWork)
        {
        }
    }
}
