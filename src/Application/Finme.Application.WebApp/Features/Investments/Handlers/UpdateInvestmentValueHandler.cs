using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Application.WebApp.Features.Investments.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Enums;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Operations.Handlers
{
    public class UpdateInvestmentValueHandler(
        IRepository<Investment> investmentRepository,
        IRepository<InvestmentStatus> investmentStatusRepository,
        IMapper mapper
    ) : IRequestHandler<UpdateInvestmentValueCommand, ResultDto<int>>
    {
        private readonly IRepository<Investment> _investmentRepository = investmentRepository;
        private readonly IRepository<InvestmentStatus> _investmentStatusRepository = investmentStatusRepository;
        private readonly IMapper _mapper = mapper; // O Mapper pode ser útil para DTOs de retorno

        public async Task<ResultDto<int>> Handle(UpdateInvestmentValueCommand request, CancellationToken cancellationToken)
        {
            // 2. Buscar o investimento existente no banco de dados
            var investment = await _investmentRepository.FindAsync(x => x.Id == request.InvestmentId);

            if (investment == null)
            {
                throw new InvalidOperationException($"Investimento com ID {request.InvestmentId} não foi encontrado.");
            }

            var newStatus = new InvestmentStatus
            {
                InvestmentId = investment.Id,
                Status = ETransactionStatus.ReportedValue, // Usamos o enum que foi convertido
                CreatedAt = DateTime.UtcNow
            };

            await _investmentStatusRepository.AddAsync(newStatus);
            await _investmentStatusRepository.SaveChangesAsync();

            investment.UpdatedAt = DateTime.UtcNow;
            investment.CommitedValue = request.CommitedValue;
            // Agora, com o ID gerado, atualizamos a referência no investimento principal
            investment.StatusId = newStatus.Id;

            // Finalmente, salvamos as alterações no investimento
            await _investmentRepository.SaveChangesAsync();

            // 6. Retornar uma resposta de sucesso
            return ResultDto<int>.SuccessResult(investment.Id, "Investimento atualizado com sucesso!");
        }
    }
}
