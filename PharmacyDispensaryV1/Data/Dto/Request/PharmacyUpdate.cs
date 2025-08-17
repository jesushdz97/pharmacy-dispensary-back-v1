namespace PharmacyDispensaryV1.Data.Dto.Request
{
    public class PharmacyUpdate
    {
        public required long PharmacyId { get; set; }
        public required string Name { get; init; }
    }
}
