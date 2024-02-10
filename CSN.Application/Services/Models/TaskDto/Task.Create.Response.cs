using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.TaskDto
{
    public class TaskCreateResponse
    {
        public bool IsSuccess { get; set; }
        public TaskCreateResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}