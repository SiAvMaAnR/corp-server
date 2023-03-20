using CSN.Domain.Entities.AttachableEntities;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Messages;

[Table("Messages")]
public partial class Message : AttachableEntity
{
    public string? Text { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsRead { get; set; }
    public User Author { get; set; } = null!;
    public int AuthorId { get; set; }
    public Message? TargetMessage { get; set; }
    public int? TargetMessageId { get; set; }
    public Channel Channel { get; set; } = null!;
    public int ChannelId { get; set; }
}
