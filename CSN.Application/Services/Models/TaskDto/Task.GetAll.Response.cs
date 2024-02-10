using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.TaskDto
{
    public class TaskGetAllResponse
    {
        public IEnumerable<TaskResponse>? Tasks { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TasksCount { get; set; }
        public int PagesCount { get; set; }
        public int InProgressCount { get; set; }
        public int CompletedCount { get; set; }
    }
}