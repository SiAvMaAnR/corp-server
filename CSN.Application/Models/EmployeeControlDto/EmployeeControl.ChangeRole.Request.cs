using CSN.Domain.Shared.Enums;

namespace CSN.Application.Models.EmployeeControlDto;

public class EmployeeControlChangeRoleRequest
{
    public int EmployeeId { get; set; }

    public EmployeeRole EmployeeRole { get; set; }
}
