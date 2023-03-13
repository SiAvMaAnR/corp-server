using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.EmployeeControlDto;

public class EmployeeControlChangeRoleResponse
{
    public int EmployeeId { get; set; }

    public string EmployeePost { get; set; } = null!;
}
