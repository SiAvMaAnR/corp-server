using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Entities.Tasks;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;

namespace CSN.Persistence.Repositories
{
    public class TaskRepository : BaseRepository<ProjectTask>, ITaskRepository
    {
        public TaskRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}