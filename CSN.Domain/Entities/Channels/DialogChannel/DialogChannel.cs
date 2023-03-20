

using System.ComponentModel.DataAnnotations.Schema;
using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Channels.DialogChannel;

[Table("DialogChannels")]
public partial class DialogChannel : Channel
{
    public User User1 { get; set; } = null!;
    public int User1Id { get; set; }
    public User User2 { get; set; } = null!;
    public int User2Id { get; set; }

}