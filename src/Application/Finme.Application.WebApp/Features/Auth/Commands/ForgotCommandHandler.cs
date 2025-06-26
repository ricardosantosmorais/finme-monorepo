using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Application.WebApp.Helpers;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using Finme.Infrastructure.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Auth.Commands
{
    public class ForgotCommandHandler(
        IRepository<User> userRepository,
        IMapper mapper,
        IRepository<VerificationCode> verificationCodeRepository,
        IVerificationService verificationService) : IRequestHandler<ForgotCommand, ForgotResponseDto>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IRepository<VerificationCode> _verificationCodeRepository = verificationCodeRepository;
        private readonly IVerificationService _verificationService = verificationService;

        public async Task<ForgotResponseDto> Handle(ForgotCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.FindAsync(u => u.Email == request.Key || u.Cpf == request.Key);
            if (existingUser == null)
                throw new Exception("Usuário não foi encontrado");

            var code = new Random().Next(100000, 999999).ToString();
            var expirationTime = DateTime.UtcNow.AddMinutes(5);

            var verificationCode = new VerificationCode
            {
                UserId = existingUser.Id,
                Code = code,
                ExpirationTime = expirationTime,
                Channel = request.Channel
            };

            await _verificationCodeRepository.AddAsync(verificationCode);
            await _verificationCodeRepository.SaveChangesAsync();

            if (request.Channel == "SMS")
                await _verificationService.EnviarCodigoPorSMS(existingUser.Telephone, code);
            else if (request.Channel == "Email")
                await _verificationService.EnviarCodigoPorEmail(existingUser.Email, code);

            existingUser.Telephone = MaskHelper.MaskContact(existingUser.Telephone);
            existingUser.Email = MaskHelper.MaskContact(existingUser.Email);

            return _mapper.Map<ForgotResponseDto>(existingUser);
        }
    }
}
