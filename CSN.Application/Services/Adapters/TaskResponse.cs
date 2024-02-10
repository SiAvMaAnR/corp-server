using System;
using System.Collections.Generic;
using System.Linq;
using CSN.Application.Services.Models.TaskDto;
using CSN.Domain.Entities.Tasks;

namespace CSN.Application.Services.Adapters
{
    public static class TaskResponseExtension
    {
        public static TaskResponse ToTaskResponse(this ProjectTask task)
        {
            return new TaskResponse()
            {
                Title = task.Title,
                Description = task.Description,
                IsDeployed = task.IsDeployed,
                EstimatedTime = task.EstimatedTime,
                Progress = task.Progress,
                Priority = task.Priority,
                ProjectId = task.ProjectId,
                State = task.State,
                TargetVersion = task.TargetVersion,
                TypeActivity = task.TypeActivity,
                UserId = task.UserId,
            };
        }
    }
}