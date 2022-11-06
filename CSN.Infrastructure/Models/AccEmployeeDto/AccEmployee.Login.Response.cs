namespace CSN.Infrastructure.Models.AccEmployeeDto;

public class AccEmployeeLoginResponse
{
    public string Token { get; set; } = null!;
    public string TokenType { get; set; } = null!;
    public bool IsSuccess { get; set; } = false;
}
