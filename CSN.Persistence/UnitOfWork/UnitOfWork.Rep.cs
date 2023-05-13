using CSN.Domain.Entities.Attachments;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Channels.DialogChannel;
using CSN.Domain.Entities.Channels.PrivateChannel;
using CSN.Domain.Entities.Channels.PublicChannel;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Notifications;
using CSN.Domain.Entities.Projects;
using CSN.Domain.Entities.Reports;
using CSN.Domain.Entities.Tasks;
using CSN.Domain.Entities.Users;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories;

namespace CSN.Persistence.UnitOfWork
{
    public partial class UnitOfWork
    {
        private readonly EFContext eFContext;

        public IUserRepository<User> User { get; }
        public ICompanyRepository Company { get; }
        public IEmployeeRepository Employee { get; }
        public IMessageRepository Message { get; }
        public IChannelRepository Channel { get; }
        public IPrivateChannelRepository PrivateChannel { get; set; }
        public IPublicChannelRepository PublicChannel { get; set; }
        public IDialogChannelRepository DialogChannel { get; set; }
        public IInvitationRepository Invitation { get; }
        public IAttachmentRepository Attachment { get; }
        public IProjectRepository Project { get; }
        public ITaskRepository Task { get; }
        public IReportRepository Report { get; }
        public INotificationRepository Notification { get; }

        public UnitOfWork(EFContext eFContext)
        {
            this.eFContext = eFContext;
            User = new UserRepository(eFContext);
            Company = new CompanyRepository(eFContext);
            Employee = new EmployeeRepository(eFContext);
            Message = new MessageRepository(eFContext);
            Channel = new ChannelRepository(eFContext);
            PrivateChannel = new PrivateChannelRepository(eFContext);
            PublicChannel = new PublicChannelRepository(eFContext);
            DialogChannel = new DialogChannelRepository(eFContext);
            Invitation = new InvitationRepository(eFContext);
            Attachment = new AttachmentRepository(eFContext);
            Project = new ProjectRepository(eFContext);
            Task = new TaskRepository(eFContext);
            Report = new ReportRepository(eFContext);
            Notification = new NotificationRepository(eFContext);
        }
    }
}
