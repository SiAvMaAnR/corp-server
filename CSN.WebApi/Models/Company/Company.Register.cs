using System.ComponentModel.DataAnnotations;

namespace CSN.WebApi.Models.Company;

public class CompanyRegister
{
    [MaxLength(25)]
    public string Login { get; set; } = null!;
    [MaxLength(35), EmailAddress]
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Image { get; set; } = null!;
    [MaxLength(400)]
    public string? Description { get; set; }
}
