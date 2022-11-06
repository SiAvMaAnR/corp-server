namespace CSN.Infrastructure.Models.AccEmployeeDto;

public class AccEmployeeRegisterRequest
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? Image { get; set; } = null!;
    public int CompanyId { get; set; }
}
