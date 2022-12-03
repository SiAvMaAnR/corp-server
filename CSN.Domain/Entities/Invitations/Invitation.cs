using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Shared.Enums;

namespace CSN.Domain.Entities.Invitations;

public partial class Invitation : BaseEntity
{
    public string Email { get; set; } = null!;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
    public EmployeeRole Role { get; set; }
    public Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
}
