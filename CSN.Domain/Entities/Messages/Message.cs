using CSN.Domain.Entities.Attachments;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Domain.Entities.Messages;

[Table("Messages")]
public partial class Message : BaseEntity
{
    public string Text { get; set; } = null!;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; }
    public bool IsRead { get; set; }
    public int ChannelId { get; set; }
    public User From { get; set; } = null!;
    public Channel Channel { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}
