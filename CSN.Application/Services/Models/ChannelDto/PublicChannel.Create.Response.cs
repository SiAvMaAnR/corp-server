using CSN.Application.Services.Models.Common;
using CSN.Domain.Entities.Channels.PublicChannel;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class PublicChannelCreateResponse : ChannelCreateResponse
    {
        public PublicChannel Channel { get; set; }
       
        public PublicChannelCreateResponse(bool isSuccess, IEnumerable<UserResponse> users, PublicChannel channel) 
            : base(isSuccess, users)
        {
            this.Users = users;
            this.Channel = channel;
        }
    }
}