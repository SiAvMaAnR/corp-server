using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Channels.DialogChannel;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class DialogChannelCreateResponse : ChannelCreateResponse
    {
        public DialogChannelCreateResponse(bool isSuccess) : base(isSuccess)
        {
        }
    }
}