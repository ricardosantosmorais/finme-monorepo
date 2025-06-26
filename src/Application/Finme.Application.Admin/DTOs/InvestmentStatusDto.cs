using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs
{
    public class InvestmentStatusDto
    {
        public int Id { get; set; }
        public int InvestmentId { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
