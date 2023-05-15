
using CSN.Domain.Common;
using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Channels.DialogChannel;

public partial class DialogChannel : IAggregateRoot
{
    public User? GetInterlocutor(User me)
    {
        return this.Users.FirstOrDefault(user => user.Id != me.Id);
    }
}