using Finme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs
{
    public class InvestmentDto
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public int UserId { get; set; }
        public decimal CommitedValue { get; set; }
        public decimal? ConfirmedValue { get; set; }
        public decimal? AssetValue { get; set; }
        public string? ContractFile { get; set; }
        public int StatusId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<InvestmentStatusDto>? InvestmentStatuses { get; set; }        
    }
}
