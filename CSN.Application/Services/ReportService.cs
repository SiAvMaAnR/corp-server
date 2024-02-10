using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Extensions;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.ReportDto;
using CSN.Domain.Entities.Reports;
using CSN.Domain.Entities.Users;
using CSN.Domain.Exceptions;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace CSN.Application.Services
{
    public class ReportService : BaseService, IReportService
    {
        public ReportService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork, context)
        {
        }


        public async Task<ReportGetAllResponse> GetReportsAsync(ReportGetAllRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork, (user) => user.Tasks) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            var reports = await this.unitOfWork.Report.GetAllAsync(
                (report) => report.TaskId == request.TaskId
                && user.Tasks.Any(task => task.Id == report.TaskId));

            if (reports == null)
                throw new NotFoundException("Tasks not found");

            int reportsCount = reports?.ToList().Count ?? 0;

            var adaptedReports = reports?
                .OrderByDescending(task => task.CreatedAt)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize);

            int pagesCount = (int)Math.Ceiling(((decimal)reportsCount / request.PageSize));

            return new ReportGetAllResponse()
            {
                Reports = adaptedReports,
                PageSize = request.PageSize,
                PageNumber = request.PageNumber,
                ReportsCount = reportsCount,
                PagesCount = pagesCount,
            };
        }

        public async Task<ReportCreateResponse> CreateReportAsync(ReportCreateRequest request)
        {
            if (this.claimsPrincipal == null)
                throw new ForbiddenException("Forbidden");

            User user = await this.claimsPrincipal.GetUserAsync(this.unitOfWork) ??
                throw new BadRequestException("Account is not found");

            int companyId = user.GetCompanyId() ??
                throw new BadRequestException("Company not found");

            Report report = new Report()
            {
                TypeActivity = request.TypeActivity,
                Comment = request.Comment,
                SpentTime = request.SpentTime,
                TaskId = request.TaskId
            };

            await this.unitOfWork.Report.AddAsync(report);
            await this.unitOfWork.SaveChangesAsync();

            return new ReportCreateResponse(true);
        }
    }
}