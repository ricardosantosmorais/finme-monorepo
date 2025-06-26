using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.DTOs.Request
{
    public class ConfirmInvestmentRequest
    {
        public int InvestmentId { get; set; }
        public int OperationId { get; set; }
        public string Code { get; set; }
    }
}
