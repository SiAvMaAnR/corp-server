using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.Common
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Image { get; set; }
        public string? Post { get; set; }
    }
}