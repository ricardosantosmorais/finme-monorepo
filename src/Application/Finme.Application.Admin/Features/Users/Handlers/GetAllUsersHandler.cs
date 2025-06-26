using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Users.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Users.Handlers
{
    public class GetAllUsersHandler(
        IRepository<User> userRepository,
        IMapper mapper
        ): IRequestHandler<GetAllUsersCommand, List<UserResponseDto>>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<List<UserResponseDto>> Handle(GetAllUsersCommand command, CancellationToken cancellationToken)
        {
            var users = await _userRepository.Get(x => !x.DeletedAt.HasValue);
            return _mapper.Map<List<UserResponseDto>>(users);
        }
    }
}
