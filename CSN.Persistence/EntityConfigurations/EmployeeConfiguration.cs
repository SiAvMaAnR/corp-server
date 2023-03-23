using CSN.Domain.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSN.Persistence.EntityConfigurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(employee => employee.Login).HasMaxLength(40).IsRequired();

            builder.HasIndex(employee => employee.Email).IsUnique();
            builder.Property(employee => employee.Email).IsRequired();

            builder.Property(employee => employee.PasswordHash).IsRequired();

            builder.Property(employee => employee.PasswordSalt).IsRequired();

            builder.HasIndex(employee => employee.Role);
            builder.Property(employee => employee.Role).IsRequired();
        }
    }
}
