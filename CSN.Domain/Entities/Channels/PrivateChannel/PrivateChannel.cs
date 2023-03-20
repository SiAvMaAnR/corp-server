


using System.ComponentModel.DataAnnotations.Schema;
using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Channels.PrivateChannel;

[Table("PrivateChannels")]
public partial class PrivateChannel : Channel
{
    public string Name { get; set; } = null!;
    public User Admin { get; set; } = null!;
    public int AdminId { get; set; }
}