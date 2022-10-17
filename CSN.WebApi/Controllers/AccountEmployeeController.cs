using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountEmployeeController : ControllerBase
    {
        [HttpPost("Login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost("Register")]
        public IActionResult Register()
        {
            return Ok();
        }

        [HttpGet("Info")]
        public IActionResult Info()
        {
            return Ok();
        }
    }
}
