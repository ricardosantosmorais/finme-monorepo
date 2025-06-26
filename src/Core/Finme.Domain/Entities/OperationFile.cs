using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Entities
{
    public class OperationFile : Base
    {
        public int OperationId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string DocumentType { get; set; }
        public string Type { get; set; }
        public string Key { get; set; }
        public string BucketName { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Operation Operation { get; set; }

    }
}
