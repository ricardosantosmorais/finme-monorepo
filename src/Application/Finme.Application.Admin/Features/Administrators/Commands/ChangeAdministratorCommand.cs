using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Administrators.Commands
{
    public class ChangeAdministratorCommand : IRequest<AdministratorDto>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Image { get; set; }
        public string Telephone { get; set; }
        public string Cpf { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
    }
}
