using CSN.Domain.Common;
using CSN.Domain.Entities.AttachableEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Attachments;

[Table("Attachments")]
public partial class Attachment : BaseEntity
{
    public string Content { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public AttachableEntity AttachableEntity { get; set; } = null!;
    public int AttachableEntityId { get; set; }
}
