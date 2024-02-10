using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelGetUsersRequest
    {
        public int ChannelId { get; set; }
        public string? Search { get; set; }

    }
}