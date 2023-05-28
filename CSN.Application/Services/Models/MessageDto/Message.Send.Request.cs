using System.Net.Mail;
namespace CSN.Application.Services.Models.MessageDto;

public class AttachmentRequest
{
    public string Content { get; set; } = null!;
    public string ContentType { get; set; } = null!;
}

public class MessageSendRequest
{
    public int ChannelId { get; set; }
    public string Text { get; set; } = null!;
    public string Html { get; set; } = null!;
    public IList<AttachmentRequest>? Attachments { get; set; }
    public int? TargetMessageId { get; set; }
}
