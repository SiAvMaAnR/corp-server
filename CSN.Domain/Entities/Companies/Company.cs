using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Entities.Projects;
using CSN.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Companies;

[Table("Companies")]
public partial class Company : User
{
    public string? Description { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
    [InverseProperty("Company")]
    public ICollection<Channel> AllChannels { get; set; } = new List<Channel>();
    [InverseProperty("Company")]
    public ICollection<Project> AllProjects { get; set; } = new List<Project>();
}
