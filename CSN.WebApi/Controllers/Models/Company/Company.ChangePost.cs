using CSN.Domain.Shared.Enums;

namespace CSN.WebApi.Controllers.Models.Company;

public class CompanyChangePost
{
    public EmployeePost EmployeePost { get; set; }

    public int EmployeeId { get; set; }
}
