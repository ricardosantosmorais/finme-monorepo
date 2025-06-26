using AutoMapper;
using Finme.Application.WebApp.Common;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.Features.Auth.Commands
{
    public class RegisterCommandHandler(IRepository<User> userRepository, IMapper mapper) : IRequestHandler<RegisterCommand, int>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Verificar se o email já existe (opcional, para evitar duplicatas)
            var existingUser = await _userRepository.FindAsync(u => u.Email == request.Email);
            if (existingUser != null)
                throw new DuplicateEmailException(request.Email);

            var existingUserCPF = await _userRepository.FindAsync(u => u.Cpf == request.Cpf);
            if (existingUserCPF != null)
                throw new DuplicateCpfException(request.Cpf);

            // Criptografar a senha com BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Criar o usuário
            var user = _mapper.Map<User>(request);

            user.CreatedAt = DateTime.UtcNow;
            user.Status = "active";
            user.Active = true;

            // Persistir no banco
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user.Id;
        }
    }
}
