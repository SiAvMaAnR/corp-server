using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ReportDto
{
    public class ReportGetAllRequest
    {
        public int TaskId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}