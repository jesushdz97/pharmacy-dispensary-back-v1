using Microsoft.EntityFrameworkCore;
using PharmacyDispensaryV1.Data.Entities;
using PharmacyDispensaryV1.Infrastructure.Database.Interceptors;

namespace PharmacyDispensaryV1.Infrastructure.Database.Context
{
    public class SqlDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new TimeStampInterceptor());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyAuditAndActiveDefaults(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void ApplyAuditAndActiveDefaults(ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>(nameof(IAuditable.CreatedAt))
                        .HasDefaultValueSql("GETDATE()");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>(nameof(IAuditable.UpdatedAt))
                        .HasDefaultValueSql("GETDATE()");
                }


                if (typeof(IActivable).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<bool>(nameof(IActivable.IsActive))
                        .HasDefaultValue(1);
                }
            }
        }

        public DbSet<Pharmacy> Pharmacy { get; set; }
    }
}
