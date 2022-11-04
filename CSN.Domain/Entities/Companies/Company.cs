using System.Text.Json.Serialization;
using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Employees;

namespace CSN.Domain.Entities.Companies;

public partial class Company : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordHash { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; } = null!;
    public string Role { get; set; } = "Company";
    public byte[]? Image { get; set; }
    public string? Description { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
