using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Entities
{
    public class Administrator : Base
    {
        public string FullName { get; set; }
        public string? Image { get; set; }
        public string Telephone { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; private set; }
        public string? Token { get; private set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }

        public Administrator() { } // Construtor padrão para AutoMapper

        public void SetToken(string token) => Token = token;
        public void UpdatePassword(string newPasswordHash) => Password = newPasswordHash;
        public void UpdateEmail(string newEmail) => Email = newEmail;
        public void UpdateTelephone(string newPhone) => Telephone = newPhone;
    }
}
