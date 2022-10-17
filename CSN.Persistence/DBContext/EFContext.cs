using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Messages;
using CSN.Persistence.DBContext;
using CSN.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Persistence.DBContext
{
    public class EFContext: DbContext
    {
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Channel> Channels { get; set; } = null!;

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguraton());
            modelBuilder.ApplyConfiguration(new ChannelConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
