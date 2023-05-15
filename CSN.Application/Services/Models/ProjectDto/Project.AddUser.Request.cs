using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ProjectDto
{
    public class ProjectAddUserRequest
    {
        public int TargetUserId { get; set; }
        public int TargetProjectId { get; set; }
    }
}