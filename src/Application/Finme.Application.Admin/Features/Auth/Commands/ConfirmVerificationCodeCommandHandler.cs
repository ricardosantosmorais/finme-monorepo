using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Auth.Commands
{
    public class ConfirmVerificationCodeCommandHandler(
        IRepository<VerificationCode> verificationCodeRepository) : IRequestHandler<ConfirmVerificationCodeCommand, bool>
    {
        private readonly IRepository<VerificationCode> _verificationCodeRepository = verificationCodeRepository;

        public async Task<bool> Handle(ConfirmVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var verificationCode = await _verificationCodeRepository.FindAsync(vc =>
                vc.UserId == request.UserId &&
                vc.Code == request.Code &&
                vc.ExpirationTime > DateTime.UtcNow);

            return verificationCode == null ? throw new Exception("Código de confirmação inválido ou expirado") : true;
        }
    }
}
