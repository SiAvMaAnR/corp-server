using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Persistence.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EFContext eFContext;
        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(EFContext eFContext, IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            this.eFContext = eFContext;
            this.employeeService = employeeService;
            this.logger = logger;
        }
    }
}
