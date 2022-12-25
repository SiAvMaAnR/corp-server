

namespace CSN.Domain.Interfaces.UnitOfWork
{
    public partial interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
