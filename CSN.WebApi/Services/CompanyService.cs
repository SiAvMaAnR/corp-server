using CSN.Domain.Entities.Companies;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.AccCompany;
using CSN.Infrastructure.Models.CompanyDto;
using CSN.Persistence.DBContext;
using CSN.WebApi.Extensions;
using CSN.WebApi.Extensions.CustomExceptions;
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

        public async Task<CompanyEmployeesResponse> EmployeesAsync(CompanyEmployeesRequest request)
        {
            Company? company = await claimsPrincipal!.GetCompanyAsync(unitOfWork, company => company.Employees);

            if (company == null)
            {
                throw new NotFoundException("Account is not found");
            }

            return new CompanyEmployeesResponse()
            {
                Employees = company.Employees
            };
        }
    }
}
