using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.AppDataDto
{
    public class UserAddRequest
    {
        public string? ConnectionId { get; set; }

        public UserAddRequest(string? connectionId)
        {
            this.ConnectionId = connectionId;
        }

        public UserAddRequest() { }
    }
}