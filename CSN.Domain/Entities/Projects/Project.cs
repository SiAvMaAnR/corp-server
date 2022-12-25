using System.ComponentModel.DataAnnotations.Schema;
using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;
using CSN.Domain.Entities.Tasks;

namespace CSN.Domain.Entities.Projects;

[Table("Projects")]
public partial class Project : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Link { get; set; }
    public ProjectState State { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
