using CSN.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Domain.Entities.Companies
{
    public interface ICompanyRepository : IAsyncRepository<Company>
    {
    }
}
