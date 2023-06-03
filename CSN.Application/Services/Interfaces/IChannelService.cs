using CSN.Application.Services.Common;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Application.Services.Models.MessageDto;

namespace CSN.Application.Services.Interfaces;

public interface IChannelService : IBaseService
{
    Task<ChannelGetAllResponse> GetAllAsync(ChannelGetAllRequest request);
    Task<ChannelGetResponse> GetAsync(ChannelGetRequest request);
    Task<PublicChannelCreateResponse> CreateAsync(PublicChannelCreateRequest request);
    Task<PrivateChannelCreateResponse> CreateAsync(PrivateChannelCreateRequest request);
    Task<DialogChannelCreateResponse> CreateAsync(DialogChannelCreateRequest request);
    Task<ChannelAddUserResponse> AddUserAsync(ChannelAddUserRequest request);
    Task<ChannelGetAllOfCompanyResponse> GetAllOfCompanyAsync(ChannelGetAllOfCompanyRequest request);
    Task<ChannelGetUsersResponse> GetUsersOfChannelAsync(ChannelGetUsersRequest request);
    Task<MessageReadResponse> ReadAsync(MessageReadRequest request);
    Task<ChannelGetUsersResponse> GetUsersNotInChannelAsync(ChannelGetUsersRequest request);
}
