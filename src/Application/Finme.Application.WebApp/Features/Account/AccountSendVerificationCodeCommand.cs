using MediatR;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class AccountSendVerificationCodeCommand : IRequest
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Channel { get; set; } // "SMS" ou "Email"
    }
}
