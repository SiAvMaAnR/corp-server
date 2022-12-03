using CSN.Domain.Shared.Enums;

namespace CSN.Infrastructure.Models.EmployeeControlDto;

public class EmployeeControlChangeRoleResponse
{
    public int EmployeeId { get; set; }

    public string EmployeeRole { get; set; } = null!;
}
