using System.ComponentModel.DataAnnotations;

namespace CSN.WebApi.Models.Employee;

public class EmployeeEdit
{
    [MaxLength(40)]
    public string Login { get; set; } = null!;
    public string? Image { get; set; }
}
