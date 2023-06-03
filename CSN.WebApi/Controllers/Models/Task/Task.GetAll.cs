using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Models.TaskDto;

namespace CSN.WebApi.Controllers.Models.Task
{
    public class TaskGetAll
    {
        public TaskFilter Filter { get; set; } = TaskFilter.GetOnlyMy;
        public string? Search { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}