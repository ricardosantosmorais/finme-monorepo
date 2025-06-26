using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.DTOs
{
    public class ChangeEmailRequestDto
    {
        public int UserId { get; set; }
        public string? Code { get; set; }
        public string Email { get; set; }
    }
}
