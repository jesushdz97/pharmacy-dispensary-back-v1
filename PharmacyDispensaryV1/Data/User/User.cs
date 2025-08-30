using PharmacyDispensaryV1.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace PharmacyDispensaryV1.Data.User
{
    public class User : IAuditable, IActivable
    {
        [Key] public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirtsName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
