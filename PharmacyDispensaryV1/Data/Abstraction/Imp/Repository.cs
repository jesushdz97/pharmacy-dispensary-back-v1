using Microsoft.EntityFrameworkCore;
using PharmacyDispensaryV1.Data.Abstraction;
using System.Linq.Expressions;

namespace PharmacyDispensaryV1.Data.Abstraction.Imp
{
    public class Repository<T>(DbContext context) : IRepository<T> where T : class
    {
        protected readonly DbContext context = context;

        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Add(IEnumerable<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(IEnumerable<T> entity)
        {
            _dbSet.UpdateRange(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public async Task<T?> Get(
            Expression<Func<T, bool>>? filter = null,
            IEnumerable<Expression<Func<T, bool>>>? filters = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Expression<Func<T, object>>[]? includes = null,
            bool tracking = false
        )
        {
            var queryable = BuildQueryable(filter, filters, orderBy, includes);
            return await queryable.FirstOrDefaultAsync();
        }

        public async Task<ICollection<T>> List(
            Expression<Func<T, bool>>? filter = null,
            IEnumerable<Expression<Func<T, bool>>>? filters = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Expression<Func<T, object>>[]? includes = null,
            bool tracking = false
        )
        {
            var queryable = BuildQueryable(filter, filters, orderBy, includes);
            return await queryable.ToListAsync();
        }

        private IQueryable<T> BuildQueryable(
            Expression<Func<T, bool>>? filter = null,
            IEnumerable<Expression<Func<T, bool>>>? filters = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Expression<Func<T, object>>[]? includes = null,
            bool tracking = false
        )
        {
            var query = tracking ? _dbSet.AsTracking() : _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (filters != null)
            {
                query = filters.Aggregate(query, (current, f) => current.Where(f));
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
    }
}
