using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Messages;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Persistence.UnitOfWork
{
    public partial class UnitOfWork : IUnitOfWork
    {
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
