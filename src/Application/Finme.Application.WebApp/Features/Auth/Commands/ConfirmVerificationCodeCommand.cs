using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Auth.Commands
{
    public class ConfirmVerificationCodeCommand : IRequest<LoginResponseDto>
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
