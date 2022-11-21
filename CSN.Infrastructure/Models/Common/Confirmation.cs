namespace CSN.Infrastructure.Models.Common;

public class Confirmation
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password{ get; set; } = null!;
    public byte[]? Image { get; set; }
    public string? Description { get; set; }
}
