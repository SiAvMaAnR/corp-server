using CSN.Domain.Common;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Channels;

[Table("Channels")]
public partial class Channel : BaseEntity
{
    public bool IsDeleted { get; set; } = false;
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
