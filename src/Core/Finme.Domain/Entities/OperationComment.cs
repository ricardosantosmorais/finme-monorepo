using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Entities
{
    public class OperationComment : Base
    {
        public int OperationId { get; set; }
        public int UserId { get; set; }
        public required string Comment { get; set; }
        public required string Date { get; set; }
        public int Likes { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public required Operation Operation { get; set; }
        public required User User { get; set; }
    }
}
