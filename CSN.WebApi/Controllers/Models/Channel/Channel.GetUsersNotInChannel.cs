using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.WebApi.Controllers.Models.Channel
{
    public class ChannelGetUsersNotInChannel
    {
        public string? Search { get; set; }
        public int ChannelId { get; set; }
    }
}