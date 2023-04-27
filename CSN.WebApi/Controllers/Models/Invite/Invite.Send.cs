using CSN.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace CSN.WebApi.Controllers.Models.Invite;

public class InviteSend
{
    [EmailAddress]
    public string Email { get; set; } = null!;

    public EmployeePost EmployeePost { get; set; } = EmployeePost.Guest;
}
