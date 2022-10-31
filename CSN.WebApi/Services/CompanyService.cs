using CSN.Domain.Entities.Companies;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Persistence.DBContext;
using CSN.WebApi.Services.Common;
using System.Security.Claims;

namespace CSN.WebApi.Services
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        public CompanyService(EFContext eFContext, IUnitOfWork unitOfWork, IHttpContextAccessor context)
            : base(eFContext, unitOfWork, context)
        {
        }

        public async Task AddAsync(Company company)
        {
            await unitOfWork.Company.AddAsync(company);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>?> GetAllAsync()
        {
            return await unitOfWork.Company.GetAllAsync();
        }
        public async Task<Company?> GetAsync(int id)
        {
            return await unitOfWork.Company.GetAsync(company => company.Id == id);
        }
    }
}
