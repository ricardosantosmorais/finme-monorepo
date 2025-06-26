using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Account.Commands
{
    public class AccountPersonaCommand : IRequest<UserDto>
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
