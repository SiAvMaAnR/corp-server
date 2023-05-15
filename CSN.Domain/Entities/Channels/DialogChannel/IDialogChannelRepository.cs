using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Domain.Interfaces.Repository;

namespace CSN.Domain.Entities.Channels.DialogChannel;


public interface IDialogChannelRepository : IAsyncRepository<DialogChannel>
{

}