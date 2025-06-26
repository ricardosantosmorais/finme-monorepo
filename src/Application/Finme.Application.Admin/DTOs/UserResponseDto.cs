using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string FullName { get; set; }
        public string? Image { get; set; }
        public string Cpf { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }

    }
}
