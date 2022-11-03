namespace CSN.Infrastructure.Models.AccCompany;

public class AccCompanyRegisterRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Image { get; set; }
    public string? Description { get; set; }
    public string Role { get; set; } = null!;
}
