using CSN.Domain.Entities.Companies;
namespace CSN.Infrastructure.Models.EmployeeDto;


public class EmployeeInfoResponse
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public byte[]? Image { get; set; }
    public int CompanyId { get; set; }
    public EmployeeCompany Company { get; set; } = null!;
}



public class EmployeeCompany
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public byte[]? Image { get; set; }
    public string? Description { get; set; }
}