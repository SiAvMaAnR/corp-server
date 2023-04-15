using CSN.Domain.Entities.Channels;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelGetAllOfCompanyResponse
    {
        public IList<Channel>? Channels { get; set; }
        public int ChannelsCount { get; set; }
    }
}
