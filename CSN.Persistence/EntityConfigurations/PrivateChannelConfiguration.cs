using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Channels.PrivateChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSN.Persistence.EntityConfigurations
{
    internal class PrivateChannelConfiguration : IEntityTypeConfiguration<PrivateChannel>
    {
        public void Configure(EntityTypeBuilder<PrivateChannel> builder)
        {
            builder.HasOne(p => p.Admin).WithMany().HasForeignKey(p => p.AdminId);

            builder.HasBaseType<Channel>();
        }
    }
}