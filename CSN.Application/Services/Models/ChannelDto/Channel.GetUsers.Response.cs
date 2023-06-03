using CSN.Application.Services.Models.Common;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelGetUsersResponse
    {
        public IList<UserResponse>? Users { get; set; }
        public int UsersCount { get; set; }
    }
}