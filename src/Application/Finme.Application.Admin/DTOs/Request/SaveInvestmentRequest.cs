using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs.Request
{
    public class SaveInvestmentRequest
    {
        public int OperationId { get; set; }
        public int UserId { get; set; }
        public decimal CommitedValue { get; set; }
        public decimal ConfirmedValue { get; set; }
        public decimal AssetValue { get; set; }
        public string ContractFile { get; set; } // Pode ser a URL ou Base64, dependendo da sua implementação
        public int StatusId { get; set; }
        public bool Active { get; set; }
    }
}
