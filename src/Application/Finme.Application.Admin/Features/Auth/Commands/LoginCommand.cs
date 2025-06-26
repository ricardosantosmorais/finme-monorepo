using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Auth.Commands
{
    public class LoginCommand : IRequest<AdministratorDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
