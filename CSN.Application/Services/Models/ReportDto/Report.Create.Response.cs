using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.ReportDto
{
    public class ReportCreateResponse
    {
        public bool IsSuccess { get; set; }
        public ReportCreateResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}