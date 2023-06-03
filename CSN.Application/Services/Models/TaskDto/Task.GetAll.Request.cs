using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.TaskDto
{
    public enum TaskFilter
    {
        GetOnlyMy,
        GetAll
    }

    public class TaskGetAllRequest
    {
        public TaskFilter Filter { get; set; } = TaskFilter.GetOnlyMy;
        public string? Search { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}