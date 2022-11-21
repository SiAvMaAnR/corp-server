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
    private readonly ILogger<TestController> logger;

    public TestController(IConfiguration configuration, IInvitationService invitationService, ILogger<TestController> logger)
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
