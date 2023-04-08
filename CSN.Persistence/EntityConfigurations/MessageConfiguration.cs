using CSN.Domain.Entities.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSN.Persistence.EntityConfigurations
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasMany(m => m.ChildMessages)
                .WithOne(m => m.TargetMessage)
                .HasForeignKey(m => m.TargetMessageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
