using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Users.Commands
{
    public class GetAllUsersCommand : IRequest<List<UserResponseDto>>
    {
    }
}
