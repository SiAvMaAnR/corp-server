using CSN.Domain.Entities.Employees;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Persistence.DBContext;
using CSN.WebApi.Services.Common;

namespace CSN.WebApi.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        public EmployeeService(EFContext eFContext, IUnitOfWork unitOfWork)
            : base(eFContext, unitOfWork)
        {
        }
    }
}
