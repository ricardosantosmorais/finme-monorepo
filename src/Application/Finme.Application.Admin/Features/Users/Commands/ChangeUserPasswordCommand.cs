using Finme.Application.Admin.DTOs;
using MediatR;
using System;

namespace Finme.Application.Admin.Features.Users.Commands
{
    public class ChangeUserPasswordCommand : IRequest<UserDto>
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}