using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
