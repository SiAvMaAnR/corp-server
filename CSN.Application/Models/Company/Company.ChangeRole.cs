using CSN.Domain.Shared.Enums;

namespace CSN.Application.Models.Company;

public class CompanyChangeRole
{
    public EmployeeRole EmployeeRole { get; set; }

    public int EmployeeId { get; set; }
}
