using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.AppContext.Models
{
    public class UserC
    {
        public int Id { get; set; }
        public string? ChatHubId { get; set; }
        public string? StateHubId { get; set; }
        public string? NotificationHubId { get; set; }
        public UserState State { get; set; } = UserState.Offline;
        public UserC(int id)
        {
            this.Id = id;
        }
    }
}