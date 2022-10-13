using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountCompanyController : ControllerBase
    {

        [HttpGet("Info")]
        public IActionResult Info()
        {
            return Ok();
        }


        [HttpGet("Register")]
        public IActionResult Register()
        {
            return Ok();
        }
    }
}
