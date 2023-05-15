namespace CSN.Application.Services.Models.CompanyDto;

public class CompanyConfirmationResponse
{
    public bool IsSuccess { get; set; } = false;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
