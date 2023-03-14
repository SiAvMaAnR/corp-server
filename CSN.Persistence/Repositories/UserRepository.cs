using CSN.Domain.Entities.Users;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;

namespace CSN.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository<User>
{
    public UserRepository(EFContext dbContext) : base(dbContext)
    {

    }
}
