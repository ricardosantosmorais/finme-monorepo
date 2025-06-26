using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Account.Commands
{
    public class AccountCommand : IRequest<AdministratorDto>
    {
        public int Id { get; set; }
    }
}
