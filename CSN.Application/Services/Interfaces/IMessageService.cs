using CSN.Application.Services.Common;
using CSN.Application.Services.Models.ChannelDto;
using CSN.Application.Services.Models.MessageDto;
namespace CSN.Application.Services.Interfaces;

public interface IMessageService : IBaseService
{
    Task<MessageSendResponse> SendAsync(MessageSendRequest request);
    Task<ChannelGetUnreadMessagesResponse> GetUnreadMessagesAsync(ChannelGetUnreadMessagesRequest request);
}