using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.MessageDto;

public class MessageSendResponse
{
    public Message Message { get; set; } = null!;

    public ICollection<User> Users { get; set; } = null!;
}