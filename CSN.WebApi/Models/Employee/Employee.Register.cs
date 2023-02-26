using System.ComponentModel.DataAnnotations;

namespace CSN.WebApi.Models.Employee;

public class EmployeeRegister
{
    [MaxLength(25)]
    public string Login { get; set; } = null!;
    public string Invite { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Image { get; set; }
}
