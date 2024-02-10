using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Models.Common;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelCreateResponse
    {
        public bool IsSuccess { get; set; } = false;
        public IEnumerable<UserResponse> Users { get; set; }

        public ChannelCreateResponse(bool isSuccess, IEnumerable<UserResponse> users)
        {
            this.IsSuccess = isSuccess;
            this.Users = users;
        }
    }
}