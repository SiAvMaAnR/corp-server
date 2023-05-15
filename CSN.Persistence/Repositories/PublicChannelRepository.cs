using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Channels.PublicChannel;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;

namespace CSN.Persistence.Repositories
{
    public class PublicChannelRepository : BaseRepository<PublicChannel>, IPublicChannelRepository
    {
        public PublicChannelRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}