using PharmacyDispensaryV1.Data.Abstraction.Imp;
using PharmacyDispensaryV1.Infrastructure.Database.Context;

namespace PharmacyDispensaryV1.Data.User.Repositories.Imp
{
    public class UserRepository(SqlDbContext context) : Repository<User>(context), IUserRepository;
}
