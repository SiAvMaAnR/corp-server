using CSN.Domain.Entities.Messages;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;

namespace CSN.Persistence.Repositories;

public class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public MessageRepository(EFContext dbContext) : base(dbContext)
    {
    }
}
