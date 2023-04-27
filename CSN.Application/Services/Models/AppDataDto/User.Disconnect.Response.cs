using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Models.AppDataDto
{
    public class UserDisconnectResponse
    {
        public bool IsSuccess { get; set; } = false;
        public UserDisconnectResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        public UserDisconnectResponse() { }
    }
}