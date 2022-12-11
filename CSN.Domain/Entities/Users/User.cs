using System.Text.Json.Serialization;
using CSN.Domain.Entities.Common;

namespace CSN.Domain.Entities.Users;

public class User : BaseEntity
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordHash { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; } = null!;
    public string Role { get; set; } = null!;
    public byte[]? Image { get; set; }
}
