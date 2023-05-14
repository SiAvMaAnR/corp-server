using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ProjectDto
{
    public class ProjectAddUserResponse
    {
        public bool IsSuccess { get; set; }

        public ProjectAddUserResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}