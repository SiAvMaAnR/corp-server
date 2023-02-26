using System.ComponentModel.DataAnnotations.Schema;
using CSN.Domain.Entities.AttachableEntities;
using CSN.Domain.Entities.Attachments;
using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Groups;

namespace CSN.Domain.Entities.Posts;

[Table("Posts")]
public partial class Post : AttachableEntity
{
    public string Text { get; set; } = null!;
    public Group Group { get; set; } = null!;
}
