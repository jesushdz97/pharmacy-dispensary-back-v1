using PharmacyDispensaryV1.Data.Entities;
using PharmacyDispensaryV1.Infrastructure.Abstraction;

namespace PharmacyDispensaryV1.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Pharmacy> Pharmacy { get; }
        Task<int> Commit();
    }
}
