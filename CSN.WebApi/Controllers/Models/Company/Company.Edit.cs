using System.ComponentModel.DataAnnotations;

namespace CSN.WebApi.Controllers.Models.Company;

public class CompanyEdit
{
    [MaxLength(40)]
    public string Login { get; set; } = null!;
    [MaxLength(400)]
    public string? Description { get; set; }
    public string? Image { get; set; }
}
