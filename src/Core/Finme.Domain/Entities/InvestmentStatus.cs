using Finme.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Entities
{
    public class InvestmentStatus : Base
    {
        public int InvestmentId { get; set; }
        public ETransactionStatus Status { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Investment Investment { get; set; }
    }
}
