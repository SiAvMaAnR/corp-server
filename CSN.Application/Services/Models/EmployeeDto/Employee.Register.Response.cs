namespace CSN.Application.Services.Models.EmployeeDto;

public class EmployeeRegisterResponse
{
    public bool IsSuccess { get; set; } = false;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
