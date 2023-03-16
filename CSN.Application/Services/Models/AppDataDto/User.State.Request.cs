using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.AppDataDto
{
    public class UserStateRequest
    {
        public UserState State { get; set; }

        public UserStateRequest(UserState state)
        {
            this.State = state;
        }

        public UserStateRequest() { }
    }
}