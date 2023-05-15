


using System.ComponentModel.DataAnnotations.Schema;
using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Channels.PrivateChannel;

[Table("PrivateChannels")]
public partial class PrivateChannel : Channel
{
    public User Admin { get; set; } = null!;
    public int AdminId { get; set; }
    public PrivateChannel()
    {
        this.LastActivity = DateTime.Now;
    }
}