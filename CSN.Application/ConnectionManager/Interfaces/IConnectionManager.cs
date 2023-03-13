using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Users;

namespace CSN.Application.ConnectionManager.Interfaces
{
    public interface IConnectionManager
    {
        void Create(User user);
        bool Remove(User user);
        List<User> Users { get; }
        IEnumerable<Company> Companies { get; }
        IEnumerable<Employee> Employees { get; }
    }
}