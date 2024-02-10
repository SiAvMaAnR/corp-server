using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Models.Common;

namespace CSN.Application.Services.Models.ProjectDto
{
    public class ProjectGetUsersResponse
    {
        public IEnumerable<UserResponse> Users { get; set; } = null!;
    }
}