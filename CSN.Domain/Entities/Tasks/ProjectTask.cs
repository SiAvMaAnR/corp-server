using System.Net.Mail;
using CSN.Domain.Common;
using CSN.Domain.Entities.Projects;
using CSN.Domain.Entities.Reports;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using CSN.Domain.Entities.AttachableEntities;

namespace CSN.Domain.Entities.Tasks;

[Table("Tasks")]
public partial class ProjectTask : AttachableEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int Progress { get; set; }
    public TaskState State { get; set; }
    public Priority Priority { get; set; } = Priority.Low;
    public ActivityType? TypeActivity { get; set; }
    public int EstimatedTime { get; set; }
    public bool? IsDeployed { get; set; }
    public string? TargetVersion { get; set; }
    public ICollection<Report> Reports { get; set; } = new List<Report>();
    public Project Project { get; set; } = null!;
    public int ProjectId { get; set; }
    public User User { get; set; } = null!;
    public int UserId { get; set; }
}
