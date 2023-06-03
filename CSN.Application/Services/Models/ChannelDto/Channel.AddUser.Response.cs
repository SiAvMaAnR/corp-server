using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Models.Common;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelAddUserResponse
    {
        public bool IsSuccess { get; set; }
        public ChannelResponseForOne? Channel { get; set; }
        public IEnumerable<UserResponse>? Users { get; set; }

        public ChannelAddUserResponse(bool isSuccess, IEnumerable<UserResponse>? users, ChannelResponseForOne? channel)
        {
            this.IsSuccess = isSuccess;
            this.Users = users;
            this.Channel = channel;
        }
    }
}