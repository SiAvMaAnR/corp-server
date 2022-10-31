using CSN.Domain.Entities.Companies;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Persistence.DBContext;
using CSN.WebApi.Models.Company;
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
        private readonly ICompanyService companyService;
        private readonly ILogger<CompanyController> logger;

        public CompanyController(EFContext eFContext, ICompanyService companyService, ILogger<CompanyController> logger)
        {
            this.eFContext = eFContext;
            this.companyService = companyService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CompanyAdd request)
        {
            try
            {
                await this.companyService.AddAsync(new Company()
                {
                    Name = request.Name,
                    Email = request.Email,
                    Image = new byte[10],
                    Description = request.Description,
                    PasswordHash = new byte[10],
                    PasswordSalt = new byte[10]
                });
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            try
            {
                Company? company = await this.companyService.GetAsync(id);
                return Ok(company);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
