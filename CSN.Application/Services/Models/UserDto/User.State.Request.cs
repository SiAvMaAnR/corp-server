using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.UserDto
{
    public class UserStateRequest
    {
        public UserStateRequest() { }
        public UserStateRequest(UserState state)
        {
            this.State = state;
        }

        public UserState State { get; set; } = UserState.Offline;
    }
}