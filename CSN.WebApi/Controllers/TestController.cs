using CSN.Email;
using CSN.Email.Models;
using CSN.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly ILogger<AccountEmployeeController> logger;

    public TestController(IConfiguration configuration, IEmailService emailService, ILogger<AccountEmployeeController> logger)
    {
        this.configuration = configuration;
        this.logger = logger;
    }

    [HttpPost]
    public IActionResult Test()
    {
        return Ok();
    }
}
