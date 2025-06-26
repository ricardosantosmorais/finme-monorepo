using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs.Request
{
    public class UpdateOperationRequestDto : CreateOperationRequestDto
    {
        public int Id { get; set; } // Id da operação a ser alterada
    }
}
