using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ProjectDto
{
    public class ProjectEditResponse
    {
        public bool IsSuccess { get; set; }

        public ProjectEditResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}