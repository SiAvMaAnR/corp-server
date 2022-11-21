using CSN.Domain.Entities.Channels;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;

namespace CSN.Persistence.Repositories;

public class ChannelRepository : BaseRepository<Channel>, IChannelRepository
{
    public ChannelRepository(EFContext dbContext) : base(dbContext)
    {
    }
}
