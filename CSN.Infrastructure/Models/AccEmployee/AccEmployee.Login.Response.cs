namespace CSN.Infrastructure.Models.AccEmployee;

public class AccEmployeeLoginResponse
{
    public string Token { get; set; } = null!;
    public string TokenType { get; set; } = null!;
    public bool IsSuccess { get; set; } = false;
}
