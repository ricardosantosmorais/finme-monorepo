using MediatR;
using System;

namespace Finme.Application.Admin.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}