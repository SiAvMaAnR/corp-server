using CSN.Application.Interfaces.Services;
using CSN.Application.Models.InvitationDto;
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
        var response = await this.invitationService.SendInviteAsync(new InvitationSendInviteRequest()
        {
            EmployeeEmail = request.Email,
            EmployeeRole = request.EmployeeRole
        });

        return Ok(new
        {
            response.IsSuccess
        });
    }

    [HttpGet("GetAll"), Authorize(Roles = "Company")]
    public async Task<IActionResult> GetInvites([FromQuery] InviteGetAll request)
    {
        var response = await this.invitationService.GetInvitesAsync(new InvitationGetAllRequest()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        });

        return Ok(new
        {
            response.PageSize,
            response.PagesCount,
            response.PageNumber,
            response.InvitationsCount,
            response.Invitations
        });
    }


    [HttpPut("State"), Authorize(Roles = "Company")]
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

    [HttpDelete("Remove"), Authorize(Roles = "Company")]
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
