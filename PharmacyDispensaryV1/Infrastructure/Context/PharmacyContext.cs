using Microsoft.EntityFrameworkCore;
using PharmacyDispensaryV1.Data.Entities;

namespace PharmacyDispensaryV1.Infrastructure.Context
{
    public class PharmacyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQL1004.site4now.net;Initial Catalog=db_abce0f_developer;User Id=db_abce0f_developer_admin;Password=Aa12345678");
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
                        .HasDefaultValue(true);
                }
            }
        }

        public DbSet<Pharmacy> Pharmacy { get; set; }
    }
}
