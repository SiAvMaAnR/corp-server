namespace CSN.Infrastructure.Models.AccCompany;

public class AccCompanyLoginResponse
{
    public string Token { get; set; } = null!;
    public string TokenType { get; set; } = null!;
    public bool IsSuccess { get; set; } = false;
}
