namespace CSN.Application.Services.Models.CompanyDto;

public class CompanyInfoResponse
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public byte[]? Image { get; set; }
    public string? Description { get; set; }
}