
namespace Finme.Domain.Entities
{
    public class VerificationCode : Base
    {
        public int? UserId { get; set; }
        public int? OperationId { get; set; }
        public int? InvestmentId { get; set; }
        public string Code { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }
        public string Channel { get; set; } // "SMS" ou "Email"
    }
}
