
using PharmacyDispensaryV1.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace PharmacyDispensaryV1.Data.Entities
{
    public class Pharmacy : IAuditable, IActivable
    {
        [Key]
        public long TestId { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
