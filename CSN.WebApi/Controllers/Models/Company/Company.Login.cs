using System.ComponentModel.DataAnnotations;
namespace CSN.WebApi.Controllers.Models.Company;

public class CompanyLogin
{
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
