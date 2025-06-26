using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Entities
{
    public class UserAddress : Base
    {
        public int UserId { get; set; }
        public string? Cep { get; set; }
        public string? Address { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        public User User { get; set; } // Relacionamento com User
    }
}
