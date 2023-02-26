using CSN.Application.Interfaces.Services;
using CSN.Application.Services.Common;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace CSN.Application.Services
{
    public class ChannelService : BaseService<Channel>, IChannelService
    {
        public ChannelService(IUnitOfWork unitOfWork, IHttpContextAccessor context)
            : base(unitOfWork, context)
        {
        }
    }
}
