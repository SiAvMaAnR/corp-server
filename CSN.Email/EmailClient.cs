using CSN.Email.Interfaces;
using CSN.Email.Models;
using MimeKit;
using MimeKit.Text;

namespace CSN.Email;

public class EmailClient
{
    private IHandler messageHandler;

    public EmailClient(IHandler handler)
    {
        this.messageHandler = handler;
    }

    public async Task SendAsync(MessageModel messageModel)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(messageModel.From.Name, messageModel.From.Email));
        emailMessage.To.Add(new MailboxAddress(messageModel.To.Name, messageModel.To.Email));
        emailMessage.Subject = messageModel.Subject;
        emailMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = messageModel.Message
        };

        await this.messageHandler.SendAsync(emailMessage);
    }
}
