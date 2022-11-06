using CSN.Domain.Entities.Companies;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.CompanyDto;
using CSN.Persistence.DBContext;
using CSN.WebApi.Models.Company;
using CSN.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly EFContext eFContext;
        private readonly ICompanyService companyService;
        private readonly IEmailService emailService;
        private readonly ILogger<CompanyController> logger;

        public CompanyController(EFContext eFContext, ICompanyService companyService, IEmailService emailService, ILogger<CompanyController> logger)
        {
            this.eFContext = eFContext;
            this.companyService = companyService;
            this.emailService = emailService;
            this.logger = logger;
        }




        [HttpPost("Invite"), Authorize(Roles = "Company")]
        public async Task<IActionResult> SendInviteAsync([FromBody] CompanyInvite invite)
        {
            await this.emailService.SendInviteAsync(invite.Email);

            return Ok();
        }



        [HttpGet("Employees"), Authorize(Roles = "Company")]
        public async Task<IActionResult> GetEmployees()
        {
            var response = await companyService.EmployeesAsync(new CompanyEmployeesRequest());

            return Ok(new
            {
                response.Employees.Count,
                response.Employees
            });
        }

    }
}
