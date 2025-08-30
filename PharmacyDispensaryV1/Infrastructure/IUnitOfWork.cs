using PharmacyDispensaryV1.Data.User.Repositories;

namespace PharmacyDispensaryV1.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }
        Task<int> Commit();
    }
}
