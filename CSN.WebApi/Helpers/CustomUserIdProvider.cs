using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace CSN.WebApi.Helpers
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}