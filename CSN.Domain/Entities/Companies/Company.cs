using System.ComponentModel.DataAnnotations.Schema;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Companies;

[Table("Companies")]
public partial class Company : User
{
    public string? Description { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
}
