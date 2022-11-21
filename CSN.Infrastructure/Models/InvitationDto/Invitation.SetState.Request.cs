namespace CSN.Infrastructure.Models.InvitationDto;

public class InvitationSetStateRequest
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public InvitationSetStateRequest(int id, bool isActive)
    {
        this.Id = id;
        this.IsActive = isActive;
    }

    public InvitationSetStateRequest() { }
}
