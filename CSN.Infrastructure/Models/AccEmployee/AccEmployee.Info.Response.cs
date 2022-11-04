using CSN.Domain.Entities.Companies;

namespace CSN.Infrastructure.Models.AccEmployee;

public class AccEmployeeInfoResponse
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public byte[]? Image { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
}
