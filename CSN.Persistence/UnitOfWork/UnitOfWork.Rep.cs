using CSN.Domain.Entities.Attachments;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Entities.Users;
using CSN.Domain.Interfaces;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.User = new UserRepository(eFContext);
            this.Company = new CompanyRepository(eFContext);
            this.Employee = new EmployeeRepository(eFContext);
            this.Message = new MessageRepository(eFContext);
            this.Channel = new ChannelRepository(eFContext);
            this.Invitation = new InvitationRepository(eFContext);
            this.Attachment = new AttachmentRepository(eFContext);
        }
    }
}
