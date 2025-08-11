using PharmacyDispensaryV1.Data.Entities;
using PharmacyDispensaryV1.Infrastructure.Abstraction;
using PharmacyDispensaryV1.Infrastructure.Context;

namespace PharmacyDispensaryV1.Infrastructure
{
    public class UnitOfWork(PharmacyContext context, PharmacyRepository pharmacy) : IUnitOfWork
    {
        public IRepository<Pharmacy> Pharmacy { get; } = pharmacy;

        private readonly PharmacyContext _context = context;

        public Task<int> Commit() => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
