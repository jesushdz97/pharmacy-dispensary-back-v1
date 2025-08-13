using PharmacyDispensaryV1.Data.Entities;
using PharmacyDispensaryV1.Infrastructure.Abstraction.Imp;
using PharmacyDispensaryV1.Infrastructure.Database.Context;

namespace PharmacyDispensaryV1.Infrastructure.Abstraction
{
    public class PharmacyRepository(SqlDbContext context) : Repository<Pharmacy>(context);
}
