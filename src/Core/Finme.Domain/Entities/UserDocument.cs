using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Entities
{
    public class UserDocument : Base
    {
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string Key { get; set; }
        public string DocumentType { get; set; }
        public string Type { get; set; }
        public string BucketName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }

        public User User { get; set; } // Relacionamento com User
    }
}
