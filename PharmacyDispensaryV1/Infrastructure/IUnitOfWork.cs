namespace PharmacyDispensaryV1.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit();
    }
}
