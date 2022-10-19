using CSN.Domain.Entities.Companies;

namespace CSN.WebApi.DTOs.Controller
{
    public class EmployeeAddRequest
    {
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Image { get; set; } = null!;
        public int CompanyId { get; set; }
    }
}
