namespace PharmacyDispensaryV1.Infrastructure
{
    public interface IAuditable
    {
        public bool IsActive { get; set; }
    }
}
