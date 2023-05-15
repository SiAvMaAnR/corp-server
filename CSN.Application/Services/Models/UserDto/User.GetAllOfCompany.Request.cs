using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.UserDto
{
    public class UserGetAllOfCompanyRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchFilter { get; set; }
    }
}