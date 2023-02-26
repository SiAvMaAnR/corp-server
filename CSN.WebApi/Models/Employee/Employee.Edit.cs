using System.ComponentModel.DataAnnotations;

namespace CSN.WebApi.Models.Employee;

public class EmployeeEdit
{
    [MaxLength(25)]
    public string Login { get; set; } = null!;
    [MaxLength(400)]
    public string? Image { get; set; }
}
