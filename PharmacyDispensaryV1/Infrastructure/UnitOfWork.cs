using PharmacyDispensaryV1.Data.User.Repositories.Imp;
using PharmacyDispensaryV1.Data.User.Repositories;
using PharmacyDispensaryV1.Infrastructure.Database.Context;

namespace PharmacyDispensaryV1.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlDbContext _context;

        public IUserRepository Users { get; }

        public UnitOfWork(SqlDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }

        public Task<int> Commit() => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
