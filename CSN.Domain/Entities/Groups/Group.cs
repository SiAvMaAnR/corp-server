using System.ComponentModel.DataAnnotations.Schema;
using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Posts;
using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Groups;

[Table("Groups")]
public partial class Group : BaseEntity
{
    public string Name { get; set; } = null!;
    public byte[]? Image { get; set; }
    public string Description { get; set; } = null!;
    public ICollection<User> Admins { get; set; } = new List<User>();
    public ICollection<User> Subscribers { get; set; } = new List<User>();
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
