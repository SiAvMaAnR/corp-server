using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.AppDataDto
{
    public class UserAddResponse
    {
        public bool IsSuccess { get; set; } = false;
        public UserAddResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        public UserAddResponse() { }
    }
}