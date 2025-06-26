
namespace Finme.Domain.Entities
{
    public class Bookmark : Base
    {
        public int UserId { get; set; }
        public int OperationId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public User User { get; set; } // Relacionamento com User
        public Operation Operation { get; set; }
    }
}
