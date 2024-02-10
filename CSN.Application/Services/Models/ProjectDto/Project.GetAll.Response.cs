using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ProjectDto
{
    public class ProjectGetAllResponse
    {
        public IEnumerable<ProjectResponse>? Projects { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int ProjectsCount { get; set; }
        public int PagesCount { get; set; }
        public int ActiveCount { get; set; }
        public int CompletedCount { get; set; }
    }
}