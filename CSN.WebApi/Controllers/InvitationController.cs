using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.InvitationDto;
using CSN.WebApi.Controllers.Models.Invite;
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

    [HttpPost("Send"), Authorize(Policy = "OnlyCompany")]
    public async Task<IActionResult> SendInvite([FromBody] InviteSend request)
    {
        var response = await invitationService.SendInviteAsync(new InvitationSendInviteRequest()
        {
            EmployeeEmail = request.Email,
            EmployeePost = request.EmployeePost
        });

        return Ok(new
        {
            response.IsSuccess
        });
    }

    [HttpGet("GetAll"), Authorize(Policy = "OnlyCompany")]
    public async Task<IActionResult> GetInvites([FromQuery] InviteGetAll request)
    {
        var response = await invitationService.GetInvitesAsync(new InvitationGetAllRequest()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
        });

        return Ok(new
        {
            response.PageSize,
            response.PagesCount,
            response.PageNumber,
            response.InvitationsCount,
            response.AcceptedCount,
            response.ActiveCount,
            response.Invitations
        });
    }


    [HttpPut("State"), Authorize(Policy = "OnlyCompany")]
    public async Task<IActionResult> SetState([FromBody] InviteSetState request)
    {
        var response = await invitationService.SetStateAsync(new InvitationSetStateRequest()
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

    [HttpDelete("Remove"), Authorize(Policy = "OnlyCompany")]
    public async Task<IActionResult> Remove([FromBody] InviteRemove request)
    {
        var response = await invitationService.RemoveInviteAsync(new InvitationRemoveRequest()
        {
            Id = request.Id,
        });

        return Ok(new
        {
            response.IsSuccess
        });
    }
}
