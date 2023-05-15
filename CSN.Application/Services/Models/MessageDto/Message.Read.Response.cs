using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.MessageDto
{
    public class MessageReadResponse
    {
        public ICollection<User> Users { get; set; } = null!;
        public IEnumerable<int> UnReadMessageIds { get; set; } = null!;
        public int UnreadMessagesCount { get; set; }
        public int ChannelId { get; set; }
    }
}