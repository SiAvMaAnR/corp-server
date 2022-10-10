using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CSN.Domain.Entities.Employees
{
    public partial class Employee : BaseEntity
    {
        public string Nickname { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Role { get; set; } = null!;
        public byte[] Image { get; set; } = null!;
        public Company Company { get; set; } = null!;
        public int CompanyId { get; set; }
    }
}
