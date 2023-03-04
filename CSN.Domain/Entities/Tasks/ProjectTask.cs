using CSN.Domain.Common;
using CSN.Domain.Entities.Projects;
using CSN.Domain.Entities.Reports;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Tasks;

[Table("Tasks")]
public partial class ProjectTask : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int Progress { get; set; }
    public TaskState State { get; set; }
    public ICollection<Report> Reports { get; set; } = new List<Report>();
    public Project Project { get; set; } = null!;
    public int ProjectId { get; set; }
    public User User { get; set; } = null!;
    public int UserId { get; set; }
}
