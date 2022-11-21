using System.ComponentModel.DataAnnotations;

namespace CSN.WebApi.Models.Invite;

public class InviteSend
{
    [MaxLength(35), EmailAddress]
    public string Email { get; set; } = null!;
}