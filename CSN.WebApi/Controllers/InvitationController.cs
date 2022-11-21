using CSN.Domain.Entities.Invitations;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.InvitationDto;
using CSN.WebApi.Models.Invite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvitationController : ControllerBase
{
    private readonly IInvitationService invitationService;
    private readonly ILogger<CompanyController> logger;


    public InvitationController(IInvitationService invitationService, ILogger<CompanyController> logger)
    {
        this.logger = logger;
        this.invitationService = invitationService;

    }

    [HttpPost("Send"), Authorize(Roles = "Company")]
    public async Task<IActionResult> SendInvite([FromBody] InviteSend request)
    {
        var response = await this.invitationService.SendInviteAsync(new InvitationSendInviteRequest(request.Email));

        return Ok(new
        {
            response.IsSuccess
        });
    }

    [HttpGet("Get"), Authorize(Roles = "Company")]
    public async Task<IActionResult> GetInvites()
    {
        var response = await this.invitationService.GetInvitesAsync(new InvitationGetAllRequest());

        return Ok(new
        {
            response.Invitations
        });
    }


    [HttpPost("State"), Authorize(Roles = "Company")]
    public async Task<IActionResult> SetState([FromBody] InviteSetState request)
    {
        var response = await this.invitationService.SetStateAsync(new InvitationSetStateRequest()
        {
            Id = request.Id,
            IsActive = request.IsActive
        });

        return Ok(new
        {
            response.Id,
            response.IsActive
        });
    }

    [HttpPost("Remove"), Authorize(Roles = "Company")]
    public async Task<IActionResult> Remove([FromBody] InviteRemove request)
    {
        var response = await this.invitationService.RemoveInviteAsync(new InvitationRemoveRequest()
        {
            Id = request.Id,
        });

        return Ok(new
        {
            response.IsSuccess
        });
    }
}
