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

        public ICompanyRepository Company { get; }
        public IEmployeeRepository Employee { get; }
        public IMessageRepository Message { get; }
        public IChannelRepository Channel { get; }

        public UnitOfWork(EFContext eFContext)
        {
            this.eFContext = eFContext;
            this.Company = new CompanyRepository(eFContext);
            this.Employee = new EmployeeRepository(eFContext);
            this.Message = new MessageRepository(eFContext);
            this.Channel = new ChannelRepository(eFContext);
        }
    }
}
