using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Channels;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelGetResponse
    {
        public Channel? Channel { get; set; }

        public ChannelGetResponse(Channel? channel)
        {
            this.Channel = channel;
        }

        public ChannelGetResponse() { }
    }
}