
using CSN.Domain.Entities.Channels.PrivateChannel;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;

namespace CSN.Persistence.Repositories
{
    public class PrivateChannelRepository : BaseRepository<PrivateChannel>, IPrivateChannelRepository
    {
        public PrivateChannelRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}