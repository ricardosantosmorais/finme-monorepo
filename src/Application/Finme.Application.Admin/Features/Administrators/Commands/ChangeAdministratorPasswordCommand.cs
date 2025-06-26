using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Administrators.Commands
{
    public class ChangeAdministratorPasswordCommand : IRequest<AdministratorDto>
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}
