using CSN.Domain.Entities.Companies;
using CSN.Persistence.DBContext;
using CSN.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly EFContext eFContext;
        private readonly CompanyService companyService;
        private readonly ILogger<CompanyController> logger;

        public CompanyController(EFContext eFContext, CompanyService companyService, ILogger<CompanyController> logger)
        {
            this.eFContext = eFContext;
            this.companyService = companyService;
            this.logger = logger;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddAsync([FromBody] Company company)
        {
            try
            {
                await this.companyService.AddAsync(company);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                IEnumerable<Company>? companies = await this.companyService.GetAllAsync();
                return Ok(companies);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
