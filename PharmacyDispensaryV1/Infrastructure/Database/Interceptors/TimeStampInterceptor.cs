using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace PharmacyDispensaryV1.Infrastructure.Database.Interceptors
{
    public class TimeStampInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default
        )
        {
            var dbContext = eventData.Context;
            if (dbContext is null) return base.SavingChangesAsync(eventData, result, cancellationToken);

            var entries = dbContext.ChangeTracker.Entries<IAuditable>();

            foreach (var entry in entries) 
            {
                if (entry.State != EntityState.Modified) continue;
                entry.Property(e => e.UpdatedAt).CurrentValue = DateTime.Now;
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
