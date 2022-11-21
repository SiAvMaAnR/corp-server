using CSN.Domain.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Persistence.EntityConfigurations
{
    internal class CompanyConfiguraton : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(company => company.Login).HasMaxLength(25).IsRequired();

            builder.HasIndex(company => company.Email).IsUnique();
            builder.Property(company => company.Email).HasMaxLength(35).IsRequired();

            builder.Property(company => company.PasswordHash).IsRequired();

            builder.Property(company => company.PasswordSalt).IsRequired();

            builder.HasIndex(company => company.Role);
            builder.Property(company => company.Role).IsRequired();

            builder.Property(company => company.Description).HasMaxLength(400);
        }
    }
}
