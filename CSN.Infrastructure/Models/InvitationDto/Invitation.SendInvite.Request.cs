namespace CSN.Infrastructure.Models.InvitationDto;

public class InvitationSendInviteRequest
{
    public string EmployeeEmail { get; set; } = null!;


    public InvitationSendInviteRequest(string email)
    {
        this.EmployeeEmail = email;
    }

    public InvitationSendInviteRequest() { }
}
