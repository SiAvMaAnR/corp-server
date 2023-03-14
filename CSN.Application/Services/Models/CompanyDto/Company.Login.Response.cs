namespace CSN.Application.Services.Models.CompanyDto;

public class CompanyLoginResponse
{
    public string Token { get; set; } = null!;
    public string TokenType { get; set; } = null!;
    public bool IsSuccess { get; set; } = false;
}
