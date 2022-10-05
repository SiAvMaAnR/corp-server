using CSN.Domain.Entities.Company;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Persistence.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}
