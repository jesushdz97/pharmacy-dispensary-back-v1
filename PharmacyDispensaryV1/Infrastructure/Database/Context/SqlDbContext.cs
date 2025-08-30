using Microsoft.EntityFrameworkCore;
using PharmacyDispensaryV1.Data.User;
using PharmacyDispensaryV1.Infrastructure.Database.Interceptors;
using PharmacyDispensaryV1.Infrastructure.Seeders;

namespace PharmacyDispensaryV1.Infrastructure.Database.Context
{
    public class SqlDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStringFactoryUtil.GetConnectionString());
            optionsBuilder.AddInterceptors(new TimeStampInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserSeeder());
            ApplyAuditAndActiveDefaults(modelBuilder);
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

        public DbSet<User> User { get; set; }
    }
}
