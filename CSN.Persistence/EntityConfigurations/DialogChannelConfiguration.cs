using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Channels.DialogChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSN.Persistence.EntityConfigurations
{
    internal class DialogChannelConfiguration : IEntityTypeConfiguration<DialogChannel>
    {
        public void Configure(EntityTypeBuilder<DialogChannel> builder)
        {
            builder.HasBaseType<Channel>();

        }
    }
}