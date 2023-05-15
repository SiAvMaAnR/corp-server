using CSN.Domain.Entities.Channels.PrivateChannel;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class PrivateChannelCreateResponse : ChannelCreateResponse
    {
        public PrivateChannel Channel { get; set; }

        public PrivateChannelCreateResponse(bool isSuccess, ICollection<User> users, PrivateChannel channel)
            : base(isSuccess, users)
        {
            this.Users = users;
            this.Channel = channel;
        }
    }
}