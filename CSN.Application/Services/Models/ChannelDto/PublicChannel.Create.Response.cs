using CSN.Domain.Entities.Channels.PublicChannel;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class PublicChannelCreateResponse : ChannelCreateResponse
    {
        public PublicChannelCreateResponse(bool isSuccess) : base(isSuccess)
        {
        }
    }
}