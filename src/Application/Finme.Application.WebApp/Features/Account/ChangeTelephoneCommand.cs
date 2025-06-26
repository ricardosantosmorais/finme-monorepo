using MediatR;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class ChangeTelephoneCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
    }
}
