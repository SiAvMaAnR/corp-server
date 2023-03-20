using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Channels.DialogChannel;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;

namespace CSN.Persistence.Repositories
{
    public class DialogChannelRepository : BaseRepository<DialogChannel>, IDialogChannelRepository
    {
        public DialogChannelRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}