using CSN.Infrastructure.Models.InvitationDto;

namespace CSN.Infrastructure.Interfaces.Services
{
    public interface IInvitationService
    {
        Task<InvitationSendInviteResponse> SendInviteAsync(InvitationSendInviteRequest request);
        Task<InvitationGetAllResponse> GetInvitesAsync(InvitationGetAllRequest request);
        Task<InvitationSetStateResponse> SetStateAsync(InvitationSetStateRequest request);
        Task<InvitationRemoveResponse> RemoveInviteAsync(InvitationRemoveRequest request);
    }
}