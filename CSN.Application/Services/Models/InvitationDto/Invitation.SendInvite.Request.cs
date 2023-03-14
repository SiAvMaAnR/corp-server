using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.InvitationDto;

public class InvitationSendInviteRequest
{
    public string EmployeeEmail { get; set; } = null!;
    public EmployeePost EmployeePost { get; set; }


    public InvitationSendInviteRequest(string email, EmployeePost role)
    {
        this.EmployeeEmail = email;
        this.EmployeePost = role;
    }

    public InvitationSendInviteRequest() { }
}
