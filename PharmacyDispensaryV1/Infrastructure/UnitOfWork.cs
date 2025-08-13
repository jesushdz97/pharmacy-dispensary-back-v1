using PharmacyDispensaryV1.Data.Entities;
using PharmacyDispensaryV1.Infrastructure.Abstraction;
using PharmacyDispensaryV1.Infrastructure.Database.Context;

namespace PharmacyDispensaryV1.Infrastructure
{
    public class UnitOfWork(SqlDbContext context, PharmacyRepository pharmacy) : IUnitOfWork
    {
        public IRepository<Pharmacy> Pharmacy { get; } = pharmacy;

        private readonly SqlDbContext _context = context;

        public Task<int> Commit() => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
