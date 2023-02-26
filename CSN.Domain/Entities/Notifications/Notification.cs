using System.ComponentModel.DataAnnotations.Schema;
using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;

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
