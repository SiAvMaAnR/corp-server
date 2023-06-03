using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.ReportDto
{
    public class ReportCreateRequest
    {
        public int SpentTime { get; set; }
        public string? Comment { get; set; }
        public ActivityType? TypeActivity { get; set; }
        public int TaskId { get; set; }
    }
}