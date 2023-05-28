using System.Threading.Channels;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;

namespace CSN.Application.Services.Models.MessageDto;

public class AttachmentResponse
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
}

public class MessageResponse
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public string? HtmlText { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsRead { get; set; } = false;
    public int AuthorId { get; set; }
    public int? TargetMessageId { get; set; }
    public int ChannelId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public IList<AttachmentResponse>? Attachments { get; set; }
}

public class MessageSendResponse
{
    public MessageResponse Message { get; set; } = null!;
    public ICollection<User> Users { get; set; } = null!;
    public int ChannelId { get; set; }
    public DateTime LastActivity { get; set; }
}