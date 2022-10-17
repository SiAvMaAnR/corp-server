using CSN.Domain.Entities.MsgContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Persistence.EntityConfigurations
{
    internal class MsgContentConfiguration : IEntityTypeConfiguration<MsgContent>
    {
        public void Configure(EntityTypeBuilder<MsgContent> builder)
        {
            throw new NotImplementedException();
        }
    }
}
