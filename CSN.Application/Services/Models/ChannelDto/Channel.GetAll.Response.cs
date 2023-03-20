using CSN.Domain.Entities.Channels;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelGetAllResponse
    {
        public IList<Channel>? Channels { get; set; }

        public ChannelGetAllResponse(IList<Channel>? channels)
        {
            this.Channels = channels;
        }

        public ChannelGetAllResponse() { }
    }
}