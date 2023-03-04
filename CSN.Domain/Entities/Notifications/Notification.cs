using CSN.Domain.Common;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Notifications;

[Table("Notifications")]
public partial class Notification : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public NotificationType Type { get; set; }
    public User User { get; set; } = null!;
    public int UserId { get; set; }
}
