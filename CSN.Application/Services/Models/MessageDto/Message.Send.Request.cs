namespace CSN.Application.Services.Models.MessageDto;

public class MessageSendRequest
{
    public int ChannelId { get; set; }
    public string Message { get; set; } = null!;
    public int? TargetMessageId { get; set; }
}
