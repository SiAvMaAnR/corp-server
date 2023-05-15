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

namespace CSN.Domain.Interfaces.UnitOfWork
{
    public partial interface IUnitOfWork
    {
        IUserRepository<User> User { get; }
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IMessageRepository Message { get; }
        IChannelRepository Channel { get; }
        IPrivateChannelRepository PrivateChannel { get; }
        IPublicChannelRepository PublicChannel { get; }
        IDialogChannelRepository DialogChannel { get; }
        IInvitationRepository Invitation { get; }
        IProjectRepository Project { get; }
        ITaskRepository Task { get; }
        IReportRepository Report { get; }
        INotificationRepository Notification { get; }
    }
}
