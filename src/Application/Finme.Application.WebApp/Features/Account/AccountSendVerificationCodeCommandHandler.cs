using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using Finme.Infrastructure.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class AccountSendVerificationCodeCommandHandler(
        IRepository<User> userRepository,
        IRepository<VerificationCode> verificationCodeRepository,
        IVerificationService verificationService) : IRequestHandler<AccountSendVerificationCodeCommand>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<VerificationCode> _verificationCodeRepository = verificationCodeRepository;
        private readonly IVerificationService _verificationService = verificationService;

        public async Task Handle(AccountSendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(u => u.Id == request.UserId);
            if (user == null)
                throw new Exception("Usuário não encontrado");

            var code = new Random().Next(100000, 999999).ToString();
            var expirationTime = DateTime.UtcNow.AddMinutes(5);

            var verificationCode = new VerificationCode
            {
                UserId = request.UserId,
                Code = code,
                CreatedAt = DateTime.UtcNow,
                Active = true,
                ExpirationTime = expirationTime,
                Channel = request.Channel
            };

            await _verificationCodeRepository.AddAsync(verificationCode);
            await _verificationCodeRepository.SaveChangesAsync();

            if (request.Channel == "SMS")
                await _verificationService.EnviarCodigoPorSMS(request.Phone, code);
            else if (request.Channel == "Email")
                await _verificationService.EnviarCodigoPorEmail(request.Email, code);
        }
    }
}
