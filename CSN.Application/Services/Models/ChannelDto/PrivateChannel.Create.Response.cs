using CSN.Domain.Entities.Channels.PrivateChannel;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class PrivateChannelCreateResponse : ChannelCreateResponse
    {
        public PrivateChannelCreateResponse(bool isSuccess) : base(isSuccess)
        {
        }
    }
}