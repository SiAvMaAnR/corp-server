namespace CSN.Application.Services.Models.EmployeeDto;

public class EmployeeLoginRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
