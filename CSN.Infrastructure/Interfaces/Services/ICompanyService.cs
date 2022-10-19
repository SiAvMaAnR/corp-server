using CSN.Domain.Entities.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Infrastructure.Interfaces.Services
{
    public interface ICompanyService
    {
        Task AddAsync(Company company);
        Task<IEnumerable<Company>?> GetAllAsync();
        Task<Company?> GetAsync(int id);
    }
}
