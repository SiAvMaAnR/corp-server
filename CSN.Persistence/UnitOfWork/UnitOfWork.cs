using CSN.Domain.Entities.Channels.DialogChannel;
using CSN.Domain.Entities.Channels.PrivateChannel;
using CSN.Domain.Entities.Channels.PublicChannel;
using CSN.Domain.Interfaces.UnitOfWork;

namespace CSN.Persistence.UnitOfWork
{
    public partial class UnitOfWork : IUnitOfWork
    {
        public async Task SaveChangesAsync()
        {
            await eFContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            eFContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                eFContext.Dispose();
            }
        }
    }
}
