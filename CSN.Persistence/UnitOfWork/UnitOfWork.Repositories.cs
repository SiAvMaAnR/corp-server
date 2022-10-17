using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Messages;
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

        private readonly ICompanyRepository companyRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMessageRepository messageRepository;
        private readonly IChannelRepository channelRepository;

        public UnitOfWork(EFContext eFContext)
        {
            this.eFContext = eFContext;
            this.companyRepository = new CompanyRepository(eFContext);
            this.employeeRepository = new EmployeeRepository(eFContext);
            this.messageRepository = new MessageRepository(eFContext);
            this.channelRepository = new ChannelRepository(eFContext);
        }

        public ICompanyRepository CompanyRepository => this.companyRepository;
        public IEmployeeRepository EmployeeRepository => this.employeeRepository;
        public IMessageRepository MessageRepository => this.messageRepository;
        public IChannelRepository ChannelRepository => this.channelRepository;
    }
}
