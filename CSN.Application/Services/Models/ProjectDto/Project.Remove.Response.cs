using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ProjectDto
{
    public class ProjectRemoveResponse
    {
        public bool IsSuccess { get; set; } = false;

        public ProjectRemoveResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}