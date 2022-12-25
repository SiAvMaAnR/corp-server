using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Channels;

[Table("Channels")]
public partial class Channel : BaseEntity
{
    public string Name { get; set; } = null!;
    public bool IsPrivate { get; set; }
    public bool IsDialog { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public ICollection<User> Users { get; set; } = new List<User>();
}
