using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Domain.Entities.Channels;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace CSN.Application.Services
{
    public class ChannelService : BaseService, IChannelService
    {
        public ChannelService(IUnitOfWork unitOfWork, IHttpContextAccessor context)
            : base(unitOfWork, context)
        {
        }
    }
}
