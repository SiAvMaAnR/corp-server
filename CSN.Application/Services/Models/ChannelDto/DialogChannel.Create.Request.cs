

using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class DialogChannelCreateRequest : ChannelCreateRequest
    {
        public int TargetUserId { get; set; } 

    }
}