using CSN.Application.Services.Models.Common;
using CSN.Domain.Entities.Channels.PrivateChannel;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class PrivateChannelCreateResponse : ChannelCreateResponse
    {
        public PrivateChannel Channel { get; set; }

        public PrivateChannelCreateResponse(bool isSuccess, IEnumerable<UserResponse> users, PrivateChannel channel)
            : base(isSuccess, users)
        {
            this.Users = users;
            this.Channel = channel;
        }
    }
}