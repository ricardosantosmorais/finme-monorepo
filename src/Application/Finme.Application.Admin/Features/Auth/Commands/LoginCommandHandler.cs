using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using Finme.Infrastructure.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Auth.Commands
{
    public class LoginCommandHandler(IRepository<Administrator> administratorRepository, IJwtService jwtService, IMapper mapper) : IRequestHandler<LoginCommand, AdministratorDto>
    {
        private readonly IRepository<Administrator> _administratorRepository = administratorRepository;
        private readonly IJwtService _jwtService = jwtService;
        private readonly IMapper _mapper = mapper;

        public async Task<AdministratorDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _administratorRepository.FindAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new Exception("E-mail ou senha inválidos");

            // Gerar o token JWT
            var token = _jwtService.GenerateToken(user);

            // Salvar o token no usuário
            user.SetToken(token);
            _administratorRepository.Update(user);
            await _administratorRepository.SaveChangesAsync();

            // Retornar o DTO com os dados do usuário e o token
            return _mapper.Map<AdministratorDto>(user);
        }
    }
}
