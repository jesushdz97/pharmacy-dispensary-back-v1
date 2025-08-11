using Microsoft.EntityFrameworkCore;
using PharmacyDispensaryV1.Data.Entities;

namespace PharmacyDispensaryV1.Infrastructure.Context
{
    public class PharmacyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-NTEH84R\\SQLEXPRESS;Database=Pharmacy;User Id=sa;Password=Aa12345;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Pharmacy> Pharmacy { get; set; }
    }
}
