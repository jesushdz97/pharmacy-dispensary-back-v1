using PharmacyDispensaryV1.Data.Entities;
using PharmacyDispensaryV1.Infrastructure;

namespace PharmacyDispensaryV1.Application.Services
{
    public class PharmacyService(IUnitOfWork uow) : IPharmacyService
    {
        private readonly IUnitOfWork _uow = uow;

        public async Task<IEnumerable<Pharmacy>> List(bool showInactive = false, bool tracking = false)
        {
            return await _uow.Pharmacy.List();
        }

        public async Task Delete(int id)
        {
            var entity = await _uow.Pharmacy.Get(x => x.TestId == id, tracking: true) ?? throw new Exception();
            entity.IsActive = false;
            await _uow.Commit();
        }

        public async Task Save(Pharmacy pharmacy)
        {
            await _uow.Pharmacy.Add(pharmacy);
            await _uow.Commit();
        }

        public async Task Update(Pharmacy pharmacy)
        {
            _uow.Pharmacy.Update(pharmacy);
            await _uow.Commit();
        }
    }
}
