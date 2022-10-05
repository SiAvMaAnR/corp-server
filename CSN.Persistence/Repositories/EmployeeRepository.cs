using CSN.Domain.Entities.Employee;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Persistence.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}
