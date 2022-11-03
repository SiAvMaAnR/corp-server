namespace CSN.WebApi.Models.AccEmployee;

public class AccEmployeeRegister
{
    public string Name { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Image { get; set; } = null!;
    public int CompanyId { get; set; }
}
