using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelGetRequest
    {
        public int Id { get; set; }

        public ChannelGetRequest(int id)
        {
            this.Id = id;
        }

        public ChannelGetRequest() { }
    }
}