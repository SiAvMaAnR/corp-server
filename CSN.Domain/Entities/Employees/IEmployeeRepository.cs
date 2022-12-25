using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Employees;

public interface IEmployeeRepository : IUserRepository<Employee>
{
}
