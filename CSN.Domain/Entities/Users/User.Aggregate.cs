using System.ComponentModel.DataAnnotations.Schema;
using CSN.Domain.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Exceptions;

namespace CSN.Domain.Entities.Users;

public partial class User : IAggregateRoot
{
    public int? GetCompanyId()
    {
        return this switch
        {
            Company => (this as Company)?.Id,
            Employee => (this as Employee)?.CompanyId,
            _  => throw new BadRequestException("Unknown type")
        };
    }
}
