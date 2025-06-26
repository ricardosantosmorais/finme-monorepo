using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class AccountCommand : IRequest<UserResponseDto>
    {
        public int Id { get; set; }
    }
}
