using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.MessageDto;

public class MessageSendResponse
{
    public Message Message { get; set; } = null!;

    public ICollection<User> Users { get; set; } = null!;

    public IEnumerable<string> ConnectionIds => this.Users
        .Where(user => user.ConnectionId != null)
        .Select(user => user.ConnectionId!);
}