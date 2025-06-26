using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Auth.Commands
{
    public class ForgotCommand : IRequest<ForgotResponseDto>
    {
        public string Key { get; set; }
        public string Channel { get; set; }
    }
}
