using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ProjectDto
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Image { get; set; }
    }


    public class ProjectGetUsersResponse
    {
        public IEnumerable<UserResponse> Users { get; set; } = null!;
    }
}