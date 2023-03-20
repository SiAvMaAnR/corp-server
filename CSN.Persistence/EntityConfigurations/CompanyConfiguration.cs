using CSN.Domain.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSN.Persistence.EntityConfigurations
{
    internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(company => company.Login).HasMaxLength(40).IsRequired();

            builder.HasIndex(company => company.Email).IsUnique();
            builder.Property(company => company.Email).HasMaxLength(80).IsRequired();

            builder.Property(company => company.PasswordHash).IsRequired();

            builder.Property(company => company.PasswordSalt).IsRequired();

            builder.HasIndex(company => company.Role);
            builder.Property(company => company.Role).IsRequired();

            builder.Property(company => company.Description).HasMaxLength(400);
        }
    }
}
