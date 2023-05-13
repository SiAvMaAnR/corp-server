using CSN.Domain.Common;
using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Channels;

public partial class Channel : IAggregateRoot
{
    public int GetUnreadMessagesCount(User targetUser)
    {
        return this.Messages.Count(message => !message.IsContainsReadUser(targetUser));
    }
}
