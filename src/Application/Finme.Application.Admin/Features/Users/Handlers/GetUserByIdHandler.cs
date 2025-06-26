using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Users.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Users.Handlers
{
    public class GetUserByIdHandler(
    IRepository<User> userRepository,
    IMapper mapper) : IRequestHandler<GetUserByIdCommand, UserDto>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithDependenciesAsync(x => x.Id == request.Id);
            if (user == null)
                throw new Exception("Usuário não encontrado!");

            return _mapper.Map<UserDto>(user);
        }
    }
}