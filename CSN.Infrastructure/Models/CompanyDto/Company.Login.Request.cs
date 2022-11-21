namespace CSN.Infrastructure.Models.CompanyDto;

public class CompanyLoginRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
