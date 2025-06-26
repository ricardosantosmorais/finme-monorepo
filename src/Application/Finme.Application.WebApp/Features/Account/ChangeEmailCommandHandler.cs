using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class ChangeEmailCommandHandler(
        IRepository<VerificationCode> verificationCodeRepository, 
        IRepository<User> userRepository) : IRequestHandler<ChangeEmailCommand, int>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<VerificationCode> _verificationCodeRepository = verificationCodeRepository;

        public async Task<int> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.FindAsync(u => u.Email == request.Email);
            if (existingUser != null)
                throw new Exception("Esse e-mail já está sendo utilizado");

            var user = await _userRepository.FindAsync(u => u.Id == request.UserId);
            if (user == null)
                throw new Exception("Usuário não foi encontrado");

            var verificationCode = await _verificationCodeRepository.FindAsync(vc =>
                vc.UserId == request.UserId &&
                vc.Code == request.Code &&
                vc.Channel == "Email" &&
                vc.ExpirationTime > DateTime.UtcNow);

            if (verificationCode == null)
            {
                throw new Exception("Código de confirmação inválido ou expirado");
            }

            // atualiza o usuário
            user.VerifyEmail = true;
            user.UpdateEmail(request.Email);
            user.UpdatedAt = DateTime.UtcNow;

            // Persistir no banco
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return user.Id;
        }
    }
}
