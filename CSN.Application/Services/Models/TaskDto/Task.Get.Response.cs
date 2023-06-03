using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.TaskDto
{
    public class TaskResponse
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Progress { get; set; }
        public TaskState State { get; set; }
        public Priority Priority { get; set; } = Priority.Low;
        public ActivityType? TypeActivity { get; set; }
        public int EstimatedTime { get; set; }
        public bool? IsDeployed { get; set; }
        public string? TargetVersion { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }


    public class TaskGetResponse
    {
        public TaskResponse? Task { get; set; }
    }
}