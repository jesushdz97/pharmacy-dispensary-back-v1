using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacyDispensaryV1.Data.User;

namespace PharmacyDispensaryV1.Infrastructure.Seeders
{
    public class UserSeeder : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    UserId = 1,
                    Email = "admin@axios.com",
                    Password = PasswordEncryptionHelper.Encrypt("Aa12345"),
                    FirtsName = "Super",
                    LastName = "Admin"
                }
            );
        }
    }
}
