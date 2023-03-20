using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Channels;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelCreateResponse
    {
        public bool IsSuccess { get; set; } = false;


        public ChannelCreateResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        public ChannelCreateResponse()
        {

        }
    }
}