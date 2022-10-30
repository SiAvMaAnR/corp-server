using CSN.Domain.Entities.Channels;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Persistence.DBContext;
using CSN.WebApi.Services.Common;
using System.Security.Claims;

namespace CSN.WebApi.Services
{
    public class ChannelService : BaseService<Channel>, IChannelService
    {
        public ChannelService(EFContext eFContext, IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal) 
            : base(eFContext, unitOfWork, claimsPrincipal)
        {
        }
    }
}
