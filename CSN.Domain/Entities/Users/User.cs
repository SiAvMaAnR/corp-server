using CSN.Domain.Common;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Channels.DialogChannel;
using CSN.Domain.Entities.Channels.PrivateChannel;
using CSN.Domain.Entities.Channels.PublicChannel;
using CSN.Domain.Entities.Groups;
using CSN.Domain.Entities.Notifications;
using CSN.Domain.Entities.Projects;
using CSN.Domain.Entities.Tasks;
using CSN.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CSN.Domain.Entities.Users;

[Table("Users")]
public partial class User : BaseEntity
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordHash { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? Image { get; set; }
    public UserState State { get; set; } = UserState.Offline;
    public ICollection<Channel> Channels { get; set; } = new List<Channel>();
    [InverseProperty("Admins")]
    public ICollection<Group> AdminGroups { get; set; } = new List<Group>();
    [InverseProperty("Subscribers")]
    public ICollection<Group> Groups { get; set; } = new List<Group>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
}
