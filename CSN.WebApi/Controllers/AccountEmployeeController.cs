using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountEmployeeController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get()
        {
            return Ok("OK");
        }
    }
}
