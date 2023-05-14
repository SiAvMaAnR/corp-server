using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Common;
using CSN.Application.Services.Models.ProjectDto;

namespace CSN.Application.Services.Interfaces
{
    public interface IProjectService : IBaseService
    {
        Task<ProjectGetAllResponse> GetProjectsAsync(ProjectGetAllRequest request);
        Task<ProjectGetResponse> GetProjectAsync(ProjectGetRequest request);
        Task<ProjectCreateResponse> CreateProjectAsync(ProjectCreateRequest request);
        Task<ProjectEditResponse> EditProjectAsync(ProjectEditRequest request);
        Task<ProjectAddUserResponse> AddUserToProjectAsync(ProjectAddUserRequest request);
        Task<ProjectRemoveResponse> RemoveProjectAsync(ProjectRemoveRequest request);
        Task<ProjectGetUsersResponse> GetUsersFromProjectAsync(ProjectGetUsersRequest request);
    }
}