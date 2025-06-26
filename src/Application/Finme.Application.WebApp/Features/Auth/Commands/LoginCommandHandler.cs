using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using Finme.Infrastructure.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Auth.Commands
{
    public class LoginCommandHandler(IRepository<User> userRepository, IJwtService jwtService, IMapper mapper) : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IJwtService _jwtService = jwtService;
        private readonly IMapper _mapper = mapper;

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new Exception("Invalid credentials");

            // Gerar o token JWT
            var token = _jwtService.GenerateToken(user);

            // Salvar o token no usuário
            user.SetToken(token);
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            // Retornar o DTO com os dados do usuário e o token
            return _mapper.Map<LoginResponseDto>(user);
        }
    }
}
