using CSN.Domain.Entities.Invitations;
using CSN.Persistence.DBContext;
using CSN.Persistence.Repositories.Common;

namespace CSN.Persistence.Repositories;

public class InvitationRepository : BaseRepository<Invitation>, IInvitationRepository
{
    public InvitationRepository(EFContext dbContext) : base(dbContext)
    {
    }
}
