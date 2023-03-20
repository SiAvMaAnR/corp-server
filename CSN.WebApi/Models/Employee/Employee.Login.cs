using System.ComponentModel.DataAnnotations;

namespace CSN.WebApi.Models.Employee;

public class EmployeeLogin
{
    [MaxLength(80), EmailAddress]
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
