using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Auth.Commands
{
    public class ChangePasswordCommandHandler(
        IRepository<Administrator> administratorRepository, 
        IRepository<VerificationCode> verificationCodeRepository) : IRequestHandler<ChangePasswordCommand, int>
    {
        private readonly IRepository<Administrator> _administratorRepository = administratorRepository;
        private readonly IRepository<VerificationCode> _verificationCodeRepository = verificationCodeRepository;

        public async Task<int> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var verificationCode = await _verificationCodeRepository.FindAsync(vc =>
               vc.UserId == request.UserId &&
               vc.Code == request.Code &&
               vc.ExpirationTime > DateTime.UtcNow);

            if (verificationCode == null)
            {
                throw new Exception("Código de confirmação inválido ou expirado");
            }
            else
            {
                // Verificar se o email já existe (opcional, para evitar duplicatas)
                var existingUser = await _administratorRepository.FindAsync(u => u.Id == request.UserId);
                if (existingUser == null)
                    throw new Exception("Usuário não foi encontrado");

                // Criptografar a senha com BCrypt
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                // atualiza o usuário
                existingUser.UpdatePassword(passwordHash);

                // Persistir no banco
                _administratorRepository.Update(existingUser);
                await _administratorRepository.SaveChangesAsync();

                return existingUser.Id;
            }                
        }
    }
}
