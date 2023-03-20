using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelAddUserRequest
    {
        public int TargetUserId { get; set; }
        public int ChannelId { get; set;}
    }
}