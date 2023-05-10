using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelGetRequest
    {
        public int Id { get; set; }
        public int Count { get; set; }

        public ChannelGetRequest(int id, int count)
        {
            this.Id = id;
            this.Count = count;
        }

        public ChannelGetRequest() { }
    }
}