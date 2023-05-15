using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ProjectDto
{
    public class ProjectCreateResponse
    {
        public bool IsSuccess { get; set; }
        public ProjectCreateResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}