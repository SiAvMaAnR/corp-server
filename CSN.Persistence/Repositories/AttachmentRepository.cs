using CSN.Domain.Entities.Attachments;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;

namespace CSN.Persistence.Repositories;

public class AttachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
{
    public AttachmentRepository(EFContext dbContext) : base(dbContext)
    {
    }
}
