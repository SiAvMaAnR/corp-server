using CSN.Application.Services.Common;
using CSN.Application.Services.Models.ChannelDto;

namespace CSN.Application.Services.Interfaces;

public interface IChannelService : IBaseService
{
    Task<ChannelGetAllResponse> GetAllAsync(ChannelGetAllRequest request);
    Task<ChannelGetResponse> GetAsync(ChannelGetRequest request);
    Task<PublicChannelCreateResponse> CreateAsync(PublicChannelCreateRequest request);
    Task<PrivateChannelCreateResponse> CreateAsync(PrivateChannelCreateRequest request);
    Task<DialogChannelCreateResponse> CreateAsync(DialogChannelCreateRequest request);
}
