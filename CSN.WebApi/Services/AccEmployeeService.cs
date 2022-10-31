using CSN.Domain.Entities.Employees;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Persistence.DBContext;
using CSN.WebApi.Services.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSN.WebApi.Services
{
    public class AccEmployeeService : BaseService<Employee>, IAccEmployeeService
    {
        private readonly IConfiguration configuration;

        public AccEmployeeService(EFContext eFContext, IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration)
            : base(eFContext, unitOfWork, context)
        {
            this.configuration = configuration;
        }
    }
}
