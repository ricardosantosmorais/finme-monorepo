using System;
using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Users.Commands
{
    public class GetUserByIdCommand : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}