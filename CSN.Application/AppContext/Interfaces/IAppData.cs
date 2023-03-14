using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Users;

namespace CSN.Application.AppData.Interfaces;

public interface IAppData
{
    List<User> Users { get; }
    IEnumerable<Company> Companies { get; }
    IEnumerable<Employee> Employees { get; }
    void Create(User user);
    bool Remove(User user);
}
