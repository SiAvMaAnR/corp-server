using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Helpers.Enums;

namespace CSN.Application.Services.Models.AppDataDto
{
    public class UserDisconnectRequest
    {
        public HubType Type { get; set; }

        public UserDisconnectRequest(HubType type)
        {
            this.Type = type;
        }
    }
}