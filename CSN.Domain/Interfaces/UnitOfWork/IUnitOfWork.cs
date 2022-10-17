using CSN.Domain.Entities.Channels;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Domain.Interfaces.UnitOfWork
{
    public partial interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
