using CSN.Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Infrastructure.Models.AccCompany
{
    public class AccCompanyInfoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = "Company";
        public byte[]? Image { get; set; }
        public string? Description { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
