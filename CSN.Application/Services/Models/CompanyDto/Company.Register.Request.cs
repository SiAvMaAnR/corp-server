namespace CSN.Application.Services.Models.CompanyDto;

public class CompanyRegisterRequest
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Description { get; set; }
}
