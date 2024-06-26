﻿using CSN.Domain.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Channels;

[Table("Channels")]
public partial class Channel : BaseEntity
{
    public string? Name { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsPublic { get; set; } = false;
    public bool IsPrivate { get; set; } = false;
    public bool IsDialog { get; set; } = false;
    public DateTime LastActivity { get; set; } = DateTime.Now;
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
}
