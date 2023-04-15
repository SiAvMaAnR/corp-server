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
        public DialogChannel Channel { get; set; }
        public DialogChannelCreateResponse(bool isSuccess, ICollection<User> users, DialogChannel channel)
            : base(isSuccess, users)
        {
            this.Users = users;
            this.Channel = channel;
        }
    }
}