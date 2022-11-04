using CSN.Domain.Entities.Employees;

namespace CSN.Infrastructure.Models.AccCompany;

public class AccCompanyEmployeesResponse
{
    public ICollection<Employee> Employees { get; set; } = null!;
}
