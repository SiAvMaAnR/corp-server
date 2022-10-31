using CSN.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSN.Domain.Entities;
using CSN.Domain.Entities.Employees;

namespace CSN.Domain.Entities.Companies
{
    public partial class Company : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Role { get; set; } = "Company";
        public byte[]? Image { get; set; }
        public string? Description { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
