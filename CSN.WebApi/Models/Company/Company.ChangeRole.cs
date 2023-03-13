using CSN.Domain.Shared.Enums;

namespace CSN.WebApi.Models.Company;

public class CompanyChangeRole
{
    public EmployeePost EmployeePost { get; set; }

    public int EmployeeId { get; set; }
}
