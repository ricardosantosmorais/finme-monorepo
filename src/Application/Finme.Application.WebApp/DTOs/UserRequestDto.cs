using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.DTOs
{
    public class UserRequestDto
    {
        public int UserId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Naturalness { get; set; } //Naturalidade
        public string? Nationality { get; set; } //Nacionalidade
        public string? Gender { get; set; } //Genero
        public string? MaritalStatus { get; set; } //Estado civil
        public string? CpfSpouse { get; set; } //CPF Conjuge
        public string? FullNameSpouse { get; set; } //Nome Conjuge
    }
}
