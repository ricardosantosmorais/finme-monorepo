using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Account.Commands
{
    public class ChangeTelephoneCommandHandler(
                IRepository<VerificationCode> verificationCodeRepository,
                IRepository<User> userRepository) : IRequestHandler<ChangeTelephoneCommand, int>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<VerificationCode> _verificationCodeRepository = verificationCodeRepository;

        public async Task<int> Handle(ChangeTelephoneCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(u => u.Id == request.UserId);
            if (user == null)
                throw new Exception("Usuário não foi encontrado");

            var verificationCode = await _verificationCodeRepository.FindAsync(vc =>
                vc.UserId == request.UserId &&
                vc.Code == request.Code &&
                vc.Channel == "SMS" &&
                vc.ExpirationTime > DateTime.UtcNow);

            if (verificationCode == null)
            {
                throw new Exception("Código de confirmação inválido ou expirado");
            }

            // atualiza o usuário
            user.VerifyPhone = true;
            user.UpdateTelephone(request.Phone);
            user.UpdatedAt = DateTime.UtcNow;

            // Persistir no banco
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return user.Id;
        }
    }
}
