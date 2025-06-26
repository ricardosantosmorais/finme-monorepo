using MediatR;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class ChangeEmailCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
