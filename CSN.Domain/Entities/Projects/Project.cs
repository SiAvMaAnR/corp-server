using CSN.Domain.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Tasks;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Projects;

[Table("Projects")]
public partial class Project : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Link { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public ProjectState State { get; set; }
    public string? Customer { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    public Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
}
