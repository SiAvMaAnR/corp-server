using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.AppDataDto
{
    public class UserRemoveResponse
    {
        public bool IsSuccess { get; set; } = false;
        public UserRemoveResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        public UserRemoveResponse() { }
    }
}