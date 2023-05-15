using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace CSN.SignalR.Hubs.Common
{
    public class BaseHub : Hub
    {
        public ClaimsPrincipal? claimsPrincipal;
    }
}