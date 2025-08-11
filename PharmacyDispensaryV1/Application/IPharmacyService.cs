using PharmacyDispensaryV1.Data.Entities;

namespace PharmacyDispensaryV1.Application
{
    public interface IPharmacyService
    {
        Task<IEnumerable<Pharmacy>> List(bool showInactive = false, bool tracking = false);
        Task Save(Pharmacy pharmacy);
        Task Delete(int id);
        Task Update(Pharmacy pharmacy);
    }
}
