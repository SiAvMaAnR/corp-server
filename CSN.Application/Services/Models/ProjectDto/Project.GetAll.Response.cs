using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ProjectDto
{
    public class ProjectGetAllResponse
    {
        public IEnumerable<ProjectResponse>? Projects { get; set; }
    }
}