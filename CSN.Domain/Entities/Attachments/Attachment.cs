using CSN.Domain.Entities.AttachableEntities;
using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Posts;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Attachments;

[Table("Attachments")]
public partial class Attachment : BaseEntity
{
    public byte[] Content { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public AttachableEntity AttachableEntity { get; set; } = null!;
    public int AttachableEntityId { get; set; }
}
