using CSN.Domain.Entities.Employees;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;

namespace CSN.Persistence.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(EFContext dbContext) : base(dbContext)
    {
    }
}
