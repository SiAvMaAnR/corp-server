using CSN.Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Infrastructure.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task AddAsync(Employee employee);
        Task<IEnumerable<Employee>?> GetAllAsync();
        Task<Employee?> GetAsync(int id);
    }
}
