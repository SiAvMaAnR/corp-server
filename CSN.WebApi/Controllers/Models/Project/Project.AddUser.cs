using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.WebApi.Controllers.Models.Project
{
    public class ProjectAddUser
    {
        public int TargetProjectId { get; set; }
        public int TargetUserId { get; set; }
    }
}