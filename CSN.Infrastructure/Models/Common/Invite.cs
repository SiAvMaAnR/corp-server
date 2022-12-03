using CSN.Domain.Shared.Enums;

namespace CSN.Infrastructure.Models.Common;

public class Invite
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Email { get; set; } = null!;
    public EmployeeRole Role { get; set; }
    public Invite() { }
    public Invite(int id, string email, int companyId, EmployeeRole role)
    {
        this.Id = id;
        this.Email = email;
        this.CompanyId = companyId;
        this.Role = role;
    }
}
