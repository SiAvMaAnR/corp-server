using CSN.Domain.Entities.Channels.PublicChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSN.Persistence.EntityConfigurations
{
    public class PublicChannelConfiguration : IEntityTypeConfiguration<PublicChannel>
    {
        public void Configure(EntityTypeBuilder<PublicChannel> builder)
        {
            
        }
    }
}