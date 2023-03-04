using CSN.Domain.Common;
using CSN.Domain.Entities.Attachments;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.AttachableEntities;

[Table("AttachableEntities")]
public partial class AttachableEntity : BaseEntity
{
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}
