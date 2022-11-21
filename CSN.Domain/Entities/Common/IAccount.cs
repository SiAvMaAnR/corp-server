namespace CSN.Domain.Entities.Common;

public interface IAccount
{
    string Login { get; set; }
    string Email { get; set; }
    byte[] PasswordHash { get; set; }
    byte[] PasswordSalt { get; set; }
    string Role { get; set; }
}