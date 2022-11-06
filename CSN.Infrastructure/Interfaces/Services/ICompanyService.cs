using CSN.Infrastructure.Models.CompanyDto;

namespace CSN.Infrastructure.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<CompanyEmployeesResponse> EmployeesAsync(CompanyEmployeesRequest request);
    }
}
