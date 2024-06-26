using System.ComponentModel.DataAnnotations;

namespace CSN.WebApi.Controllers.Models.Employee;

public class EmployeeLogin
{
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
