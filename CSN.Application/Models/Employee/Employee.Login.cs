using System.ComponentModel.DataAnnotations;

namespace CSN.Application.Models.Employee;

public class EmployeeLogin
{
    [MaxLength(35), EmailAddress]
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
