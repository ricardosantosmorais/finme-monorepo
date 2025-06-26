using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Administrators.Commands
{
    public class GetAllAdministratorsCommand : IRequest<List<AdministratorDto>>
    {
    }
}
