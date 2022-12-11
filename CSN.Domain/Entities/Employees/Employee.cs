using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Employees;

[Table("Employees")]
public partial class Employee : User
{
    public Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
}