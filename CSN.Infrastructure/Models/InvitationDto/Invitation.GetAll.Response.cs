using CSN.Domain.Entities.Invitations;

namespace CSN.Infrastructure.Models.InvitationDto;

public class InvitationGetAllResponse
{
    public IEnumerable<Invitation>? Invitations { get; set; } = null!;

    public InvitationGetAllResponse(IEnumerable<Invitation>? invitations)
    {
        this.Invitations = invitations;
    }

    public InvitationGetAllResponse() { }
}
