using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs.Request
{
    public class SaveBookmarkRequest
    {
        public int UserId { get; set; }
        public int OperationId { get; set; }
    }
}
