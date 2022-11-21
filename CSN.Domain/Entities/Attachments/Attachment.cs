using CSN.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Domain.Entities.Attachments;

public partial class Attachment : BaseEntity
{
    public byte[] Content { get; set; } = null!;
    //ArrByte Content
    //Type content string
    //MessageID
}
