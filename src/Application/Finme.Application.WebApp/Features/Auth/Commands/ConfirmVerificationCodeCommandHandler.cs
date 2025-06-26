using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using Finme.Infrastructure.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Auth.Commands
{
    public class ConfirmVerificationCodeCommandHandler(
        IRepository<User> userRepository,
        IRepository<VerificationCode> verificationCodeRepository,
        IJwtService jwtService,
        IMapper mapper) : IRequestHandler<ConfirmVerificationCodeCommand, LoginResponseDto>
    {
        private readonly IRepository<VerificationCode> _verificationCodeRepository = verificationCodeRepository;
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<LoginResponseDto> Handle(ConfirmVerificationCodeCommand request, CancellationToken cancellationToken)
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
                var user = await _userRepository.FindAsync(u => u.Id == request.UserId) ?? throw new Exception("Usuário não encontrado");

                // Gerar o token JWT
                var token = _jwtService.GenerateToken(user);

                // Salvar o token no usuário
                user.SetToken(token);

                if(verificationCode.Channel == "Email")
                {
                    user.VerifyEmail = true;
                }
                else if (verificationCode.Channel == "SMS")
                {
                    user.VerifyPhone = true;
                }

                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                // Retornar o DTO com os dados do usuário e o token
                return _mapper.Map<LoginResponseDto>(user);
            }
        }
    }
}
