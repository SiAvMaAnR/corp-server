using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Reports;

namespace CSN.Application.Services.Models.ReportDto
{
    public class ReportGetAllResponse
    {
        public IEnumerable<Report>? Reports { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int ReportsCount { get; set; }
        public int PagesCount { get; set; }
    }
}