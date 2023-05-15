using CSN.Domain.Common;
using CSN.Domain.Entities.Tasks;
using CSN.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Reports;

[Table("Reports")]
public partial class Report : BaseEntity
{
    public int SpentTime { get; set; }
    public string? Comment { get; set; }
    public ActivityType? TypeActivity { get; set; }
    public ProjectTask Task { get; set; } = null!;
    public int TaskId { get; set; }
}
