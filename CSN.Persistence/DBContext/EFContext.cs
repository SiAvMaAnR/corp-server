using CSN.Domain.Entities.Attachments;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Channels.DialogChannel;
using CSN.Domain.Entities.Channels.PrivateChannel;
using CSN.Domain.Entities.Channels.PublicChannel;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Projects;
using CSN.Domain.Entities.Tasks;
using CSN.Domain.Entities.Users;
using CSN.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CSN.Persistence.DBContext
{
    public class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Channel> Channels { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<ProjectTask> Tasks { get; set; } = null!;
        public DbSet<PublicChannel> PublicChannels { get; set; } = null!;
        public DbSet<PrivateChannel> PrivateChannels { get; set; } = null!;
        public DbSet<DialogChannel> DialogChannels { get; set; } = null!;
        public DbSet<Attachment> Attachments { get; set; } = null!;
        public DbSet<Invitation> Invitations { get; set; } = null!;

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelConfiguration());
            modelBuilder.ApplyConfiguration(new PrivateChannelConfiguration());
            modelBuilder.ApplyConfiguration(new PublicChannelConfiguration());
            modelBuilder.ApplyConfiguration(new DialogChannelConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new InvitationConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
