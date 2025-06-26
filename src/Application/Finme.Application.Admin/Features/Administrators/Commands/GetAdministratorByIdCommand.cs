using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Administrators.Commands
{
    public class GetAdministratorByIdCommand : IRequest<AdministratorDto>
    {
        public int Id { get; set; }
    }
}
