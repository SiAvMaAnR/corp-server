using CSN.Domain.Entities.AttachableEntities;
using CSN.Domain.Entities.Attachments;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Common;
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
    public Message? Answer { get; set; }
    public int? AnswerId { get; set; }
    public Channel Channel { get; set; } = null!;
    public int ChannelId { get; set; }
}
