using CSN.Domain.Entities.Attachments;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Entities.Messages;
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
        public IInvitationRepository Invitation { get; }
        public IAttachmentRepository Attachment { get; }

        public UnitOfWork(EFContext eFContext)
        {
            this.eFContext = eFContext;
            User = new UserRepository(eFContext);
            Company = new CompanyRepository(eFContext);
            Employee = new EmployeeRepository(eFContext);
            Message = new MessageRepository(eFContext);
            Channel = new ChannelRepository(eFContext);
            Invitation = new InvitationRepository(eFContext);
            Attachment = new AttachmentRepository(eFContext);
        }
    }
}
