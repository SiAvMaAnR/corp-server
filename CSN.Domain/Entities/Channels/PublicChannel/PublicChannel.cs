

using System.ComponentModel.DataAnnotations.Schema;
using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Channels.PublicChannel;


[Table("PublicChannels")]
public partial class PublicChannel : Channel
{
    public User Admin { get; set; } = null!;
    public int AdminId { get; set; }
    public PublicChannel()
    {
        this.LastActivity = DateTime.Now;
    }
}