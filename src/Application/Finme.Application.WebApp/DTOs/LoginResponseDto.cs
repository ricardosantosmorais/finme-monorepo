using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.DTOs
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Token { get; set; }
        public string FullName { get; set; }
    }
}
