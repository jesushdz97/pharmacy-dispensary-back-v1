using PharmacyDispensaryV1.Data.Dto.Request;
using PharmacyDispensaryV1.Data.Entities;

namespace PharmacyDispensaryV1.Data.Mappers
{
    public static class PharmacyMappingExtensions
    {
        public static Pharmacy ToPharmacyCreate(this PharmacyCreate request)
        {
            return new Pharmacy
            {
                Name = request.Name,
            };
        }

        public static Pharmacy ToPharmacyUpdate(this PharmacyUpdate request)
        {
            return new Pharmacy
            {
                PharmacyId = request.PharmacyId,
                Name = request.Name,
            };
        }
    }
}
