using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Domain.Entities.Attachments;

public partial class Attachment : BaseEntity
{
    public byte[] Content { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public int MessageId { get; set; }
    public Message Message { get; set; } = null!;
}
