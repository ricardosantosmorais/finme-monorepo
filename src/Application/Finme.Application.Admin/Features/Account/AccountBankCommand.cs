using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Account.Commands
{
    public class AccountBankCommand : IRequest<UserDto>
    {
        public int UserId { get; set; }
        public string Bank { get; set; }
        public string Agency { get; set; }
        public string CurrentAccount { get; set; }
        public bool JoinAccount { get; set; }
        public string? CpfSpouse { get; set; } //CPF Conjuge
        public string? FullNameSpouse { get; set; } //Nome Conjuge
    }
}
