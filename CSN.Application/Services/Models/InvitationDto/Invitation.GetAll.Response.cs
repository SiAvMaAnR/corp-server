using CSN.Domain.Entities.Invitations;

namespace CSN.Application.Services.Models.InvitationDto;

public class InvitationGetAllResponse
{
    public IEnumerable<Invitation>? Invitations { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int InvitationsCount { get; set; }
    public int AcceptedCount { get; set; }
    public int ActiveCount { get; set; }
    public int PagesCount { get; set; }
}
