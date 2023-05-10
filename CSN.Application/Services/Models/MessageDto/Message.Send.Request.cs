namespace CSN.Application.Services.Models.MessageDto;

public class MessageSendRequest
{
    public int ChannelId { get; set; }
    public string Text { get; set; } = null!;
    public string Html { get; set; } = null!;
    public int? TargetMessageId { get; set; }
}
