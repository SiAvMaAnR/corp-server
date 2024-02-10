using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Extensions;
using CSN.Application.Services.Adapters;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.TaskDto;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Tasks;
using CSN.Domain.Entities.Users;
using CSN.Domain.Exceptions;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Domain.Shared.Enums;
using Microsoft.AspNetCore.Http;

namespace CSN.Application.Services
{
    public class TaskService : BaseService, ITaskService
    {
        public TaskService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork, context)
        {

        }


        public async Task<TaskGetAllResponse> GetTasksAsync(TaskGetAllRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            string searchField = request.Search?.ToLower() ?? "";

            var tasks = await this.unitOfWork.Task.GetAllAsync(
                (task) =>
                    task.Project.CompanyId == companyId &&
                    (request.Filter == TaskFilter.GetAll || task.UserId == user.Id) &&
                    task.Title.ToLower().Contains(searchField),
                (task) => task.Project);

            if (tasks == null)
                throw new NotFoundException("Tasks not found");

            int tasksCount = tasks?.ToList().Count ?? 0;

            int tasksIsProgressCount = tasks?.Count(task => task.State == TaskState.InProgress) ?? 0;
            int tasksCompletedCount = tasks?.Count(task => task.State == TaskState.Deployed
                || task.State == TaskState.ReadyToDeploy) ?? 0;

            var adaptedTasks = tasks?
                .OrderByDescending(task => task.CreatedAt)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .Select(task => task.ToTaskResponse());

            int pagesCount = (int)Math.Ceiling(((decimal)tasksCount / request.PageSize));

            return new TaskGetAllResponse()
            {
                Tasks = adaptedTasks,
                PageSize = request.PageSize,
                PageNumber = request.PageNumber,
                TasksCount = tasksCount,
                InProgressCount = tasksIsProgressCount,
                CompletedCount = tasksCompletedCount,
                PagesCount = pagesCount,
            };
        }

        public async Task<TaskGetResponse> GetTaskAsync(TaskGetRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            ProjectTask? task = await this.unitOfWork.Task.GetAsync(
                (task) =>
                    task.Project.CompanyId == companyId &&
                    task.Id == request.TaskId,
                (task) => task.Project);

            return new TaskGetResponse()
            {
                Task = task?.ToTaskResponse()
            };
        }


        public async Task<TaskCreateResponse> CreateTaskAsync(TaskCreateRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            ProjectTask task = new ProjectTask()
            {
                Title = request.Title,
                Description = request.Description,
                EstimatedTime = request.EstimatedTime,
                IsDeployed = request.IsDeployed,
                Priority = request.Priority,
                Progress = request.Progress,
                ProjectId = request.ProjectId,
                State = request.State,
                TargetVersion = request.TargetVersion,
                TypeActivity = request.TypeActivity,
                UserId = request.TargetUserId,
            };

            await this.unitOfWork.Task.AddAsync(task);
            await this.unitOfWork.SaveChangesAsync();

            return new TaskCreateResponse(true);
        }


        public async Task<TaskEditResponse> EditTaskAsync(TaskEditRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            ProjectTask? task = await this.unitOfWork.Task.GetAsync(
                (task) =>
                    task.Project.CompanyId == companyId &&
                    task.Id == request.TaskId,
                (task) => task.Project);

            if (task == null)
                throw new BadRequestException("");

            task.UserId = request.TargetUserId;
            task.Description = request.Description;
            task.EstimatedTime = request.EstimatedTime;
            task.Priority = request.Priority;
            task.Progress = request.Progress;
            task.State = request.State;
            task.TypeActivity = request.TypeActivity;
            task.TargetVersion = request.TargetVersion;
            task.Title = request.Title;

            await this.unitOfWork.Task.UpdateAsync(task);
            await this.unitOfWork.SaveChangesAsync();

            return new TaskEditResponse(true);
        }
    }
}