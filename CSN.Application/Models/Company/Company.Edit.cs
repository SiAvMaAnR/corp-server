using System.ComponentModel.DataAnnotations;

namespace CSN.Application.Models.Company;

public class CompanyEdit
{
    [MaxLength(25)]
    public string Login { get; set; } = null!;
    [MaxLength(400)]
    public string? Description { get; set; }
    public string? Image { get; set; }
}
