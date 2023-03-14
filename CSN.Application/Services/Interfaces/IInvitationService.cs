using CSN.Application.Services.Models.InvitationDto;

namespace CSN.Application.Services.Interfaces;

public interface IInvitationService
{
    Task<InvitationSendInviteResponse> SendInviteAsync(InvitationSendInviteRequest request);
    Task<InvitationGetAllResponse> GetInvitesAsync(InvitationGetAllRequest request);
    Task<InvitationSetStateResponse> SetStateAsync(InvitationSetStateRequest request);
    Task<InvitationRemoveResponse> RemoveInviteAsync(InvitationRemoveRequest request);
}