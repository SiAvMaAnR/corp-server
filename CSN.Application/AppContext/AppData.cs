using CSN.Application.AppData.Interfaces;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Users;

namespace CSN.Application.AppData;

public class AppData : IAppData
{
    public List<User> Users { get; } = new List<User>();
    public IEnumerable<Company> Companies => this.Users.OfType<Company>();
    public IEnumerable<Employee> Employees => this.Users.OfType<Employee>();

    public void Create(User user)
    {
        this.Users.Add(user);
    }

    public bool Remove(User user)
    {
        return this.Users.Remove(user);
    }
}
