using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.AppDataDto
{
    public class UserStateResponse
    {
        public bool IsSuccess { get; set; } = false;
        public UserStateResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        public UserStateResponse() { }
    }
}