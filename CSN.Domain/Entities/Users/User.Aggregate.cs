using CSN.Domain.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;

namespace CSN.Domain.Entities.Users;

public partial class User : IAggregateRoot
{
    public int? CompanyId => ((this as Company)?.Id ?? (this as Employee)?.CompanyId);
}
