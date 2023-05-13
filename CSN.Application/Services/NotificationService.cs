using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace CSN.Application.Services
{
    public class NotificationService : BaseService, INotificationService
    {

        public NotificationService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork, context)
        {
        }
    }
}