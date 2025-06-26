using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs.Request
{
    public class UpdateInvestmentRequest
    {
        public int Id { get; set; } // ID do investimento a ser atualizado
        public int OperationId { get; set; }
        public int UserId { get; set; }
        public decimal? ConfirmedValue { get; set; }
        public decimal? AssetValue { get; set; }
        public string? ContractFile { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }
    }
}
