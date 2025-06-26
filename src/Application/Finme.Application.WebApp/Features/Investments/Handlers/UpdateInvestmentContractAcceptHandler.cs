using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Application.WebApp.Features.Investments.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Enums;
using Finme.Domain.Interfaces;
using Finme.Infrastructure.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Operations.Handlers
{
    public class UpdateInvestmentContractAcceptHandler(
        IRepository<Investment> investmentRepository,
         IRepository<User> userRepository,
        IRepository<InvestmentStatus> investmentStatusRepository,
        IVerificationService verificationService,
        IRepository<VerificationCode> verificationCodeRepository,
        IMapper mapper
    ) : IRequestHandler<UpdateInvestmentContractAcceptCommand, ResultDto<int>>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<Investment> _investmentRepository = investmentRepository;
        private readonly IRepository<InvestmentStatus> _investmentStatusRepository = investmentStatusRepository;
        private readonly IRepository<VerificationCode> _verificationCodeRepository = verificationCodeRepository;
        
        private readonly IVerificationService _verificationService = verificationService;
        private readonly IMapper _mapper = mapper; // O Mapper pode ser útil para DTOs de retorno

        public async Task<ResultDto<int>> Handle(UpdateInvestmentContractAcceptCommand request, CancellationToken cancellationToken)
        {
            // 2. Buscar o investimento existente no banco de dados
            var investment = await _investmentRepository.FindAsync(x => x.Id == request.InvestmentId);

            if (investment == null)
            {
                throw new InvalidOperationException($"Investimento com ID {request.InvestmentId} não foi encontrado.");
            }

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            //Salvando o novo status
            var newStatus = new InvestmentStatus
            {
                InvestmentId = investment.Id,
                Status = ETransactionStatus.ContractAccept, // Usamos o enum que foi convertido
                CreatedAt = DateTime.UtcNow
            };

            await _investmentStatusRepository.AddAsync(newStatus);
            await _investmentStatusRepository.SaveChangesAsync();

            investment.UpdatedAt = DateTime.UtcNow;
            investment.StatusId = newStatus.Id;

            await _investmentRepository.SaveChangesAsync();

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
                Channel = "SMS"
            };

            await _verificationCodeRepository.AddAsync(verificationCode);
            await _verificationCodeRepository.SaveChangesAsync();

            await _verificationService.EnviarCodigoPorSMS(user.Telephone, code);

            return ResultDto<int>.SuccessResult(investment.Id, "Investimento atualizado com sucesso!");
        }
    }
}
