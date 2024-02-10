using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Common;
using CSN.Application.Services.Models.ReportDto;

namespace CSN.Application.Services.Interfaces
{
    public interface IReportService : IBaseService
    {
        Task<ReportGetAllResponse> GetReportsAsync(ReportGetAllRequest request);
        Task<ReportCreateResponse> CreateReportAsync(ReportCreateRequest request);
    }
}