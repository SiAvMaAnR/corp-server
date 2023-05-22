using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Models.ProjectDto;
using CSN.Domain.Entities.Projects;

namespace CSN.Application.Services.Adapters
{
    public static class ProjectResponseExtension
    {
        public static ProjectResponse ToProjectResponse(this Project project)
        {
            return new ProjectResponse()
            {
                Id = project.Id,
                Name = project.Name,
                Customer = project.Customer,
                CompanyId = project.CompanyId,
                Description = project.Description,
                Link = project.Link,
                Priority = project.Priority,
                State = project.State,
            };
        }
    }
}