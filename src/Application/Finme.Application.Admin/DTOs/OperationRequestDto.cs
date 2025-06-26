using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs
{
    public class OperationRequestDto
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
    }
}
