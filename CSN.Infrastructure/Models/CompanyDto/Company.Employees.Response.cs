using CSN.Domain.Entities.Employees;

namespace CSN.Infrastructure.Models.CompanyDto;

public class CompanyEmployeesResponse
{
    public ICollection<Employee> Employees { get; set; } = null!;
}
