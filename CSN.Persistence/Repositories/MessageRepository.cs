using CSN.Domain.Entities.Messages;
using CSN.Domain.Interfaces;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Persistence.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}
