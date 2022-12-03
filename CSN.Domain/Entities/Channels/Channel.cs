using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Domain.Entities.Channels;

public partial class Channel : BaseEntity
{
    public string Name { get; set; } = null!;
    public bool IsPrivate { get; set; }
    public bool IsDialog { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsRead { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
