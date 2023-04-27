using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelAddUserResponse
    {
        public bool IsSuccess { get; set; }
        public Channel? Channel { get; set; }
        public ICollection<User>? Users { get; set; }

        public ChannelAddUserResponse(bool isSuccess, ICollection<User>? users, Channel? channel)
        {
            this.IsSuccess = isSuccess;
            this.Users = users;
            this.Channel = channel;
        }
    }
}