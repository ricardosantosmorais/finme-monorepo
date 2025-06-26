using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.DTOs
{
    public class BookmarkDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationId { get; set; }
        public OperationDto Operation { get; set; }
    }
}
