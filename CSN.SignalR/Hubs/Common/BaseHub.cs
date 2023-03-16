using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Common;
using Microsoft.AspNetCore.SignalR;

namespace CSN.SignalR.Hubs.Common
{
    public class BaseHub : Hub
    {
        public ClaimsPrincipal? claimsPrincipal;
    }
}