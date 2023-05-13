using System.Threading.Channels;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.MessageDto;

public class MessageResponse
{
    public string? Text { get; set; }
    public string? HtmlText { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsRead { get; set; } = false;
    public int AuthorId { get; set; }
    public int? TargetMessageId { get; set; }
    public int ChannelId { get; set; }
}

public class MessageSendResponse
{
    public MessageResponse Message { get; set; } = null!;
    public ICollection<User> Users { get; set; } = null!;
    public int ChannelId { get; set; }
    public DateTime LastActivity { get; set; }
}