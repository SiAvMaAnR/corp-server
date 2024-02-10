using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.WebApi.Controllers.Models.Project
{
    public class ProjectGetAll
    {
        public string? SearchFilter { get; set; }
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}