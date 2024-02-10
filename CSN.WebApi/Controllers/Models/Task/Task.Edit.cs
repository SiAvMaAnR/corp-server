using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Shared.Enums;

namespace CSN.WebApi.Controllers.Models.Task
{
    public class TaskEdit
    {
        public int TargetUserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Progress { get; set; }
        public TaskState State { get; set; }
        public Priority Priority { get; set; }
        public ActivityType? TypeActivity { get; set; }
        public int EstimatedTime { get; set; }
        public bool? IsDeployed { get; set; }
        public string? TargetVersion { get; set; }
    }
}