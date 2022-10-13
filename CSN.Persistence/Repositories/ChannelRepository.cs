using CSN.Domain.Entities.Channels;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Persistence.Repositories
{
    public class ChannelRepository : BaseRepository<Channel>, IChannelRepository
    {
        public ChannelRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}
