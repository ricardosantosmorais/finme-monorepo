using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Entities
{
    public class Investment : Base
    {
        public int OperationId { get; set; }
        public int UserId { get; set; }
        public decimal CommitedValue { get; set; }
        public decimal? ConfirmedValue { get; set; }
        public decimal? DividendsReceived { get; set; }
        public decimal? AssetValue { get; set; }
        public string? ContractFile { get; set; }
        public int? StatusId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<InvestmentStatus>? InvestmentStatuses { get; set; }
        public Operation? Operation { get; set; }
    }
}
