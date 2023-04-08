using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelAddUserResponse
    {
        public bool IsSuccess { get; set; }

        public ChannelAddUserResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        public ChannelAddUserResponse() { }
    }
}