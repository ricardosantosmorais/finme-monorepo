using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Auth.Commands
{
    public class ConfirmVerificationCodeCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
