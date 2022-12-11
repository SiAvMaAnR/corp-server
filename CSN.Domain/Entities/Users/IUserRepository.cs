using CSN.Domain.Interfaces.Repository;

namespace CSN.Domain.Entities.Users;

public interface IUserRepository<TUser> : IAsyncRepository<TUser> where TUser : User
{
}
