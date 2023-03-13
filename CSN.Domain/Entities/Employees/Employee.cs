using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Employees;

[Table("Employees")]
public partial class Employee : User
{
    public Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
    public EmployeePost Post { get; set; }
}