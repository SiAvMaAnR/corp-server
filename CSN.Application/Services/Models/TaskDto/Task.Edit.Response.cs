using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.TaskDto
{
    public class TaskEditResponse
    {
        public bool IsSuccess { get; set; }

        public TaskEditResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}