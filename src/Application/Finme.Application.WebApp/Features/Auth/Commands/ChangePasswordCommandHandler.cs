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
    public class ChangePasswordCommandHandler(IRepository<User> userRepository) : IRequestHandler<ChangePasswordCommand, int>
    {
        private readonly IRepository<User> _userRepository = userRepository;

        public async Task<int> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            // Verificar se o email já existe (opcional, para evitar duplicatas)
            var existingUser = await _userRepository.FindAsync(u => u.Id == request.UserId);
            if (existingUser == null)
                throw new Exception("Usuário não foi encontrado");

            // Criptografar a senha com BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // atualiza o usuário
            existingUser.UpdatePassword(passwordHash);

            // Persistir no banco
            _userRepository.Update(existingUser);
            await _userRepository.SaveChangesAsync();

            return existingUser.Id;
        }
    }
}
