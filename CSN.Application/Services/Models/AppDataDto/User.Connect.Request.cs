using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Helpers.Enums;

namespace CSN.Application.Services.Models.AppDataDto
{
    public class UserConnectRequest
    {
        public string? ConnectionId { get; set; }

        public HubType Type { get; set; }

        public UserConnectRequest(string? connectionId, HubType type)
        {
            this.ConnectionId = connectionId;
            this.Type = type;
        }

        public UserConnectRequest() { }
    }
}