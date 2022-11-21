using CSN.Domain.Entities.Companies;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;

namespace CSN.Persistence.Repositories;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    public CompanyRepository(EFContext dbContext) : base(dbContext)
    {
    }
}
