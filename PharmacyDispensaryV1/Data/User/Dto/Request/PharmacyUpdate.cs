namespace PharmacyDispensaryV1.Data.User.Dto.Request
{
    public class PharmacyUpdate
    {
        public required long PharmacyId { get; set; }
        public required string Name { get; init; }
    }
}
