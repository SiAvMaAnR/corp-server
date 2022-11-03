namespace CSN.Infrastructure.Models.AccCompany;

public class AccCompanyInfoResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public byte[]? Image { get; set; }
    public string? Description { get; set; }
}