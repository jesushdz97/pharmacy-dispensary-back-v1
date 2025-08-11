using PharmacyDispensaryV1.Infrastructure.Context;

namespace PharmacyDispensaryV1.Infrastructure.Imp
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PharmacyContext context;

        public UnitOfWork(PharmacyContext context) => this.context = context;

        public Task<int> Commit() => context.SaveChangesAsync();

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
