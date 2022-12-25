using CSN.Domain.Entities.Attachments;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Entities.Messages;


namespace CSN.Domain.Interfaces.UnitOfWork
{
    public partial interface IUnitOfWork
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IMessageRepository Message { get; }
        IChannelRepository Channel { get; }
        IInvitationRepository Invitation { get; }
        IAttachmentRepository Attachment { get; }
    }
}
