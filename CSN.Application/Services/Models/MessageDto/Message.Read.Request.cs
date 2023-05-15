using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.MessageDto
{
    public class MessageReadRequest
    {
        public int ChannelId { get; set; }

        public MessageReadRequest(int id)
        {
            this.ChannelId = id;
        }
    }
}