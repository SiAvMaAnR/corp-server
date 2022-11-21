namespace CSN.Infrastructure.Models.CompanyDto;

public class CompanyRegisterRequest
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Image { get; set; }
    public string? Description { get; set; }
}
