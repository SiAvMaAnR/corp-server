using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Groups;
using CSN.Domain.Entities.Notifications;
using CSN.Domain.Entities.Projects;
using CSN.Domain.Entities.Tasks;

namespace CSN.Domain.Entities.Users;

[Table("Users")]
public class User : BaseEntity
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordHash { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; } = null!;
    public string Role { get; set; } = null!;
    public byte[]? Image { get; set; }
    public ICollection<Channel> Channels { get; set; } = new List<Channel>();
    [InverseProperty("Admins")]
    public ICollection<Group> AdminGroups { get; set; } = new List<Group>();
    [InverseProperty("Subscribers")]
    public ICollection<Group> Groups { get; set; } = new List<Group>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
}
