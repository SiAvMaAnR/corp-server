using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelGetUsersResponse
    {
        public IList<User>? Users { get; set; }
        public int UsersCount { get; set; }
    }
}