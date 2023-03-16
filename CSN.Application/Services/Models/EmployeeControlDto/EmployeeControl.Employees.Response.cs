using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.EmployeeControlDto;

public class EmployeeControlEmployeesResponse
{
    public IList<CompanyEmployee>? Employees { get; set; } = null;
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int EmployeesCount { get; set; }
    public int OnlineCount { get; set; }
    public int PagesCount { get; set; }
}


public class CompanyEmployee
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public byte[]? Image { get; set; } = null!;
    public UserState State { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}