using CSN.Domain.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Invitations;

[Table("Invitations")]
public partial class Invitation : BaseEntity
{
    public string Email { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public EmployeeRole Role { get; set; }
    public Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
}
