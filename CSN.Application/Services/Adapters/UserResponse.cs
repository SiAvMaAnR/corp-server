using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Models.ProjectDto;
using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Adapters
{
    public static class UserResponseExtension
    {
        public static UserResponse ToUserResponse(this User user)
        {
            return new UserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Image = user.Image,
                Role = user.Role,
            };
        }
    }
}