using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Interfaces;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Infrastructure.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext eFContext;

        private readonly ICompanyRepository companyRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMessageRepository messageRepository;

        public UnitOfWork(EFContext eFContext)
        {
            this.eFContext = eFContext;
            this.companyRepository = new CompanyRepository(eFContext);
            this.employeeRepository = new EmployeeRepository(eFContext);
            this.messageRepository = new MessageRepository(eFContext);
        }

        public ICompanyRepository CompanyRepository => this.companyRepository;
        public IEmployeeRepository EmployeeRepository => this.employeeRepository;
        public IMessageRepository MessageRepository => this.messageRepository;

        public async Task SaveChangesAsync()
        {
            await this.eFContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            this.eFContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.eFContext.Dispose();
            }
        }
    }
}
