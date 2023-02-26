namespace CSN.Application.Models.CompanyDto;

public class CompanyEditRequest
{
    public string Login { get; set; } = null!;
    public string? Image { get; set; } = null!;
    public string? Description { get; set; }
}
