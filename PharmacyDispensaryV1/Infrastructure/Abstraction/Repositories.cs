using PharmacyDispensaryV1.Data.Entities;
using PharmacyDispensaryV1.Infrastructure.Abstraction.Imp;
using PharmacyDispensaryV1.Infrastructure.Context;

namespace PharmacyDispensaryV1.Infrastructure.Abstraction
{
    public class PharmacyRepository(PharmacyContext context) : Repository<Pharmacy>(context);
}
