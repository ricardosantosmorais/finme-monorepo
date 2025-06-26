using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Entities
{
    public class UserBank : Base
    {
        public int UserId { get; set; }
        public string Bank { get; set; }
        public string Agency { get; set; }
        public string CurrentAccount { get; set; }
        public bool JoinAccount { get; set; }
        public string? CpfSpouse { get; set; } //CPF Conjuge
        public string? FullNameSpouse { get; set; } //Nome Conjuge

        public User User { get; set; } // Relacionamento com User
    }
}
