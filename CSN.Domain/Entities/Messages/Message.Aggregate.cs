using CSN.Domain.Common;
using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Messages;


public partial class Message : IAggregateRoot
{
    public bool IsContainsReadUser(User targetUser)
    {
        return this.ReadUsers.Any(user => user.Id == targetUser.Id);
    }
}
