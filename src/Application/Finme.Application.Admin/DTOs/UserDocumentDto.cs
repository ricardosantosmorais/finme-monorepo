using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs
{
    public class UserDocumentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string DocumentType { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
    }
}
