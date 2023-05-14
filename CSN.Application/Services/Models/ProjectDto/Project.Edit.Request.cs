using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.ProjectDto
{
    public class ProjectEditRequest
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Link { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;
        public ProjectState State { get; set; }
        public string? Customer { get; set; }
    }
}