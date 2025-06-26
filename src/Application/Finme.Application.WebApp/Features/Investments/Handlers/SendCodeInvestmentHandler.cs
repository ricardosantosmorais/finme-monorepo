using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Application.WebApp.Features.Investments.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using Finme.Infrastructure.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Investments.Handlers
{
    public class SendCodeInvestmentHandler(
         IRepository<User> userRepository,
        IVerificationService verificationService,
        IRepository<VerificationCode> verificationCodeRepository
    ) : IRequestHandler<SendCodeInvestmentCommand, ResultDto<bool>>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<VerificationCode> _verificationCodeRepository = verificationCodeRepository;        
        private readonly IVerificationService _verificationService = verificationService;

        public async Task<ResultDto<bool>> Handle(SendCodeInvestmentCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            //Enviando código de confirmação para o usuário
            var code = new Random().Next(100000, 999999).ToString();
            var expirationTime = DateTime.UtcNow.AddMinutes(5);

            var verificationCode = new VerificationCode
            {
                UserId = request.UserId,
                OperationId = request.OperationId,
                InvestmentId = request.InvestmentId,
                Code = code,
                CreatedAt = DateTime.UtcNow,
                Active = true,
                ExpirationTime = expirationTime,
                Channel = request.Channel // "SMS" ou "Email"
            };

            await _verificationCodeRepository.AddAsync(verificationCode);
            await _verificationCodeRepository.SaveChangesAsync();

            if (request.Channel == "SMS")
                await _verificationService.EnviarCodigoPorSMS(user.Telephone, code);
            else if (request.Channel == "Email")
                await _verificationService.EnviarCodigoPorEmail(user.Email, code);

            return ResultDto<bool>.SuccessResult(true, "Código enviado com sucesso!");
        }
    }
}
