using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Common;
using CSN.Application.Services.Models.TaskDto;

namespace CSN.Application.Services.Interfaces
{
    public interface ITaskService : IBaseService
    {
        Task<TaskGetAllResponse> GetTasksAsync(TaskGetAllRequest request);
        Task<TaskGetResponse> GetTaskAsync(TaskGetRequest request);
        Task<TaskCreateResponse> CreateTaskAsync(TaskCreateRequest request);
        Task<TaskEditResponse> EditTaskAsync(TaskEditRequest request);
    }
}