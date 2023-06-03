using System.Data;
using CSN.Application.Extensions;
using CSN.Application.Services.Adapters;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.ProjectDto;
using CSN.Domain.Entities.Projects;
using CSN.Domain.Entities.Users;
using CSN.Domain.Exceptions;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Domain.Shared.Enums;
using Microsoft.AspNetCore.Http;

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
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            string searchField = request.Search?.ToLower() ?? "";

            var projects = await this.unitOfWork.Project.GetAllAsync((project) =>
                project.CompanyId == companyId &&
                (project.Users.Contains(user) || user.Role == "Company") &&
                project.Name.ToLower().Contains(searchField));

            if (projects == null)
                throw new NotFoundException("Projects not found");

            int projectsCount = projects?.ToList().Count ?? 0;

            int projectsActiveCount = projects?.Count(project => project.State == ProjectState.Active) ?? 0;
            int projectsCompletedCount = projects?.Count(project => project.State == ProjectState.Completed) ?? 0;

            var adaptedProjects = projects?
                .OrderByDescending(project => project.CreatedAt)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .Select(project => project.ToProjectResponse());

            int pagesCount = (int)Math.Ceiling(((decimal)projectsCount / request.PageSize));

            return new ProjectGetAllResponse()
            {
                Projects = adaptedProjects,
                PageSize = request.PageSize,
                PageNumber = request.PageNumber,
                ProjectsCount = projectsCount,
                ActiveCount = projectsActiveCount,
                CompletedCount = projectsCompletedCount,
                PagesCount = pagesCount,
            };
        }

        public async Task<ProjectGetResponse> GetProjectAsync(ProjectGetRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            Project? project = await this.unitOfWork.Project.GetAsync((project) =>
                project.Id == request.ProjectId &&
                (project.Users.Contains(user) || user.Role == "Company") &&
                project.CompanyId == companyId);

            return new ProjectGetResponse()
            {
                Project = project?.ToProjectResponse()
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
                State = request.State ?? ProjectState.Active,
                Priority = Priority.Default,
                CompanyId = companyId,
            };

            await this.unitOfWork.Project.AddAsync(project);
            await this.unitOfWork.SaveChangesAsync();

            return new ProjectCreateResponse(true);
        }

        public async Task<ProjectEditResponse> EditProjectAsync(ProjectEditRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            Project? project = await this.unitOfWork.Project.GetAsync(
               (project) => project.CompanyId == companyId
                    && project.Id == request.ProjectId
            ) ?? throw new NotFoundException("Project not found");

            project.Name = request.Name;
            project.Description = request.Description;
            project.Customer = request.Customer;
            project.Link = request.Link;
            project.Priority = request.Priority;
            project.State = request.State;

            await this.unitOfWork.Project.UpdateAsync(project);
            await this.unitOfWork.SaveChangesAsync();

            return new ProjectEditResponse(true);
        }

        public async Task<ProjectGetUsersResponse> GetUsersFromProjectAsync(ProjectGetUsersRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork,
                (user) => user.Projects) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            var users = await this.unitOfWork.User.GetAllAsync(
                (user) => user.Projects.Any(
                    (project) => project.Id == request.ProjectId));

            if (users == null)
                throw new NotFoundException("Users not found");

            var adaptUsers = users.Select(user => user.ToUserResponse());

            return new ProjectGetUsersResponse()
            {
                Users = adaptUsers
            };
        }

        public async Task<ProjectAddUserResponse> AddUserToProjectAsync(ProjectAddUserRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            Project? project = await this.unitOfWork.Project.GetAsync(
                (project) => project.CompanyId == companyId && project.Id == request.TargetProjectId,
                (project) => project.Users) ??
                throw new NotFoundException("Project not found");

            User? targetUser = await this.unitOfWork.User.GetAsync(
                (user) => user.Id == request.TargetUserId
                    && user.GetCompanyId() == companyId) ??
                throw new NotFoundException("User not found");

            if (project.Users.Contains(targetUser))
                throw new BadRequestException("This user already connected");

            project.Users.Add(targetUser);

            await this.unitOfWork.SaveChangesAsync();

            return new ProjectAddUserResponse(true);
        }

        public async Task<ProjectRemoveResponse> RemoveProjectAsync(ProjectRemoveRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            var project = await this.unitOfWork.Project.GetAsync(
                (project) => project.CompanyId == companyId && project.Id == request.ProjectId) ??
                throw new NotFoundException("Project not found");

            await this.unitOfWork.Project.DeleteAsync(project);
            await this.unitOfWork.SaveChangesAsync();

            return new ProjectRemoveResponse(true);
        }
    }
}