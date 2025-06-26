using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Users.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Users.Handlers
{
    public class ChangeUserPasswordHandler(
        IRepository<User> userRepository,
        IMapper mapper) : IRequestHandler<ChangeUserPasswordCommand, UserDto>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto> Handle(ChangeUserPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.Id == command.Id);
            if (user == null)
                throw new Exception("Usuário não encontrado!");

            // Criptografar a senha com BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);
            user.UpdatePassword(passwordHash); // Usa o método da entidade para atualizar a senha
            user.UpdatedAt = DateTime.UtcNow;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}