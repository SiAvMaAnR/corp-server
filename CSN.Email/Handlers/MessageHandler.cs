using CSN.Email.Interfaces;
using CSN.Email.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CSN.Email.Handlers;

public class MessageHandler : IHandler
{
    private SmtpModel smtp;

    public MessageHandler(SmtpModel smtp)
    {
        this.smtp = smtp;
    }

    public async Task SendAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(smtp.Host, smtp.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(smtp.Email, smtp.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
