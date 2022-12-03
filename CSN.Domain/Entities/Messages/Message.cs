using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Common;
using CSN.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Domain.Entities.Messages;

public partial class Message : BaseEntity
{

    public string Content { get; set; } = null!;

    public int ChannelId { get; set; }

    public Channel Channel { get; set; } = null!;
}
