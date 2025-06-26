using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.DTOs.Request
{
    public class UpdateInvestmentRequest
    {
        public int InvestmentId { get; set; }
        public int OperationId { get; set; }
        public decimal? CommitedValue { get; set; }
    }
}
