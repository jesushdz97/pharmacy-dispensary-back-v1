using PharmacyDispensaryV1.Data.Dto.Request;
using PharmacyDispensaryV1.Data.Entities;

namespace PharmacyDispensaryV1.Data.Mappers
{
    public static class PharmacyMappingExtensions
    {
        public static Pharmacy ToPharmacy(this PharmacyRequest request)
        {
            return new Pharmacy
            {
                Name = request.Name,
            };
        }
    }
}
