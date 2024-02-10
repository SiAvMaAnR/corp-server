using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.ProjectDto
{
    public class ProjectCreateRequest
    {
        public string Name { get; set; } = null!;
        public string? Customer { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public ProjectState? State { get; set; }
    }
}