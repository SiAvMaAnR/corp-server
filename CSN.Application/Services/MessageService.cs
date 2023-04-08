

using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CSN.Application.Services;

public class MessageService : BaseService, IMessageService
{
    public readonly IConfiguration configuration;

    public MessageService(IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration)
    : base(unitOfWork, context)
    {
        this.configuration = configuration;
    }
}