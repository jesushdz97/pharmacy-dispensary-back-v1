using System.Linq.Expressions;

namespace PharmacyDispensaryV1.Data.Abstraction
{
    public interface IRepository<T> where T : class
    {
        public Task Add(T entity);
        public Task Add(IEnumerable<T> entity);
        public void Update(T entity);
        public void Update(IEnumerable<T> entity);
        public void Delete(T entity);
        public void Delete(IEnumerable<T> entity);

        Task<ICollection<T>> List(
            Expression<Func<T, bool>>? filter = null,
            IEnumerable<Expression<Func<T, bool>>>? filters = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Expression<Func<T, object>>[]? includes = null,
            bool tracking = false
        );

        Task<T?> Get(
            Expression<Func<T, bool>>? filter = null,
            IEnumerable<Expression<Func<T, bool>>>? filters = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Expression<Func<T, object>>[]? includes = null,
            bool tracking = false
        );
    }
}
