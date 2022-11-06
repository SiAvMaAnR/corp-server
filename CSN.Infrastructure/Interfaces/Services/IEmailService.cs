namespace CSN.Infrastructure.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendInviteAsync(string email);
    }
}
