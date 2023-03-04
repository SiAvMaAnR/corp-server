using CSN.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSN.Domain.Entities.Reactions;

[Table("Reactions")]
public partial class Reaction : BaseEntity
{
    public byte[] Content { get; set; } = null!;
    public string Type { get; set; } = null!;
}
