using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Auth.Commands
{
    public class ForgotCommand : IRequest<ForgotResponseDto>
    {
        public string Key { get; set; }
        public string Channel { get; set; }
    }
}
