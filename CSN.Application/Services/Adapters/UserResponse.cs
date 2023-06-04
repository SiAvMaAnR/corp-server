using CSN.Application.Services.Models.Common;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Users;
using CSN.Persistence.Extensions;

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
                Image = Convert.ToBase64String(user.Image.ReadToBytes() ?? new byte[0]),
                Role = user.Role,
                Post = (user as Employee)?.Post.ToString()
            };
        }
    }
}