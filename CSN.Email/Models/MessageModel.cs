namespace CSN.Email.Models;

public class MessageModel
{
    public AddressModel From { get; set; } = null!;
    public AddressModel To { get; set; } = null!;
    public string Subject { get; set; } = "";
    public string Message { get; set; } = null!;
}
