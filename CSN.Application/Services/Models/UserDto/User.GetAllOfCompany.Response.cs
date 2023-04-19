using CSN.Domain.Entities.Users;

namespace CSN.Application.Services.Models.UserDto
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public byte[]? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
    }


    public class UserGetAllOfCompanyResponse
    {
        public ICollection<UserResponse>? Users { get; set; } = new List<UserResponse>();
        public int UsersCount { get; set; }
    }
}