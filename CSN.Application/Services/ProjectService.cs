using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Extensions;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.ProjectDto;
using CSN.Domain.Entities.Projects;
using CSN.Domain.Entities.Users;
using CSN.Domain.Exceptions;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Domain.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CSN.Application.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(IUnitOfWork unitOfWork, IHttpContextAccessor context)
            : base(unitOfWork, context)
        {

        }


        public async Task<ProjectGetAllResponse> GetProjectsAsync(ProjectGetAllRequest request)
        {

            return new ProjectGetAllResponse()
            {

            };
        }


        public async Task<ProjectGetResponse> GetProjectAsync(ProjectGetRequest request)
        {

            return new ProjectGetResponse()
            {

            };
        }

        public async Task<ProjectCreateResponse> CreateProjectAsync(ProjectCreateRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            Project project = new Project()
            {
                Name = request.Name,
                Customer = request.Customer,
                Description = request.Description,
                Link = request.Link,
                State = ProjectState.Active,
                Priority = Priority.Default,
                CompanyId = companyId
            };

            await this.unitOfWork.Project.AddAsync(project);

            await this.unitOfWork.SaveChangesAsync();

            return new ProjectCreateResponse()
            {

            };
        }

        public async Task<ProjectEditResponse> EditProjectAsync(ProjectEditRequest request)
        {
            return new ProjectEditResponse()
            {

            };
        }
    }
}