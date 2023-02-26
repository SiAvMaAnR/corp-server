using CSN.Domain.Shared.Enums;

namespace CSN.Application.Models.InvitationDto;

public class InvitationSendInviteRequest
{
    public string EmployeeEmail { get; set; } = null!;
    public EmployeeRole EmployeeRole { get; set; }


    public InvitationSendInviteRequest(string email, EmployeeRole role)
    {
        this.EmployeeEmail = email;
        this.EmployeeRole = role;
    }

    public InvitationSendInviteRequest() { }
}
