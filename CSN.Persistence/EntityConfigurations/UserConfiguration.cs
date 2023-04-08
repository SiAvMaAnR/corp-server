using CSN.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSN.Persistence.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.UseTpcMappingStrategy();
            builder.HasIndex(user => user.Email).IsUnique();
            builder.HasMany(user => user.Channels).WithMany(channel => channel.Users);
            builder.Ignore(user => user.ConnectionId);
        }
    }
}