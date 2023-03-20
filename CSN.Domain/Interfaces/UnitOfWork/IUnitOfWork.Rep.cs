using CSN.Domain.Entities.Attachments;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Channels.DialogChannel;
using CSN.Domain.Entities.Channels.PrivateChannel;
using CSN.Domain.Entities.Channels.PublicChannel;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Entities.Messages;
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
        IAttachmentRepository Attachment { get; }
    }
}
