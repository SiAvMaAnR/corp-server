using CSN.Domain.Common;
using CSN.Domain.Entities.Users;

namespace CSN.Domain.Entities.Channels;

public partial class Channel : IAggregateRoot
{
    public void UpdateUnreadMessagesCount(User targetUser)
    {
        this.UnreadMessagesCount = Messages.Count(message => message.IsContainsReadUser(targetUser) == false);
    }
}
