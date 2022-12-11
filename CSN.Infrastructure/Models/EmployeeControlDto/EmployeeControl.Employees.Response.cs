using CSN.Domain.Entities.Employees;

namespace CSN.Infrastructure.Models.EmployeeControlDto;

public class EmployeeControlEmployeesResponse
{
    public IList<CompanyEmployee> Employees { get; set; } = null!;
}


public class CompanyEmployee
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public byte[]? Image { get; set; } = null!;
}