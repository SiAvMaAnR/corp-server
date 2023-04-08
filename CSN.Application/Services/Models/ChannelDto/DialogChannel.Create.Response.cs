using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Channels.DialogChannel;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class DialogChannelCreateResponse : ChannelCreateResponse
    {
        public ICollection<User> Users { get; set; }
        public DialogChannelCreateResponse(bool isSuccess, ICollection<User> users) : base(isSuccess)
        {
            this.Users = users;
        }
    }
}