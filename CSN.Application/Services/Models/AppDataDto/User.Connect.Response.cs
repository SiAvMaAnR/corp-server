using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.AppDataDto
{
    public class UserConnectResponse
    {
        public bool IsSuccess { get; set; } = false;
        public UserConnectResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        public UserConnectResponse() { }
    }
}