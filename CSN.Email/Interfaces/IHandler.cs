using MimeKit;

namespace CSN.Email.Interfaces;

public interface IHandler
{
    Task SendAsync(MimeMessage message);
}
