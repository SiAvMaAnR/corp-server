using System.ComponentModel.DataAnnotations;

namespace CSN.WebApi.Models.Company;

public class CompanyRegister
{
    [MaxLength(40)]
    public string Login { get; set; } = null!;
    [MaxLength(80), EmailAddress]
    public string Email { get; set; } = null!;
    [MaxLength(80), EmailAddress]
    public string Password { get; set; } = null!;
    [MaxLength(400)]
    public string? Description { get; set; }
}
