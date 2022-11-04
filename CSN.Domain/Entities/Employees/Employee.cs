using System.Text.Json.Serialization;
using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Companies;

namespace CSN.Domain.Entities.Employees;

public partial class Employee : BaseEntity
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordHash { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; } = null!;
    public string Role { get; set; } = null!;
    public byte[]? Image { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
}