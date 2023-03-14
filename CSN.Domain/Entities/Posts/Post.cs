using CSN.Domain.Entities.AttachableEntities;
using CSN.Domain.Entities.Groups;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Posts;

[Table("Posts")]
public partial class Post : AttachableEntity
{
    public string Text { get; set; } = null!;
    public Group Group { get; set; } = null!;
}
