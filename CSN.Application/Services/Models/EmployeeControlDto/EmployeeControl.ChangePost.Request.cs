using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.EmployeeControlDto;

public class EmployeeControlChangePostRequest
{
    public int EmployeeId { get; set; }

    public EmployeePost EmployeePost { get; set; }
}
