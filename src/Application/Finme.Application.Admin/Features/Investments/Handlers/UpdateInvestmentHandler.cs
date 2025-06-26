using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Investments.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Enums;
using Finme.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Features.Investments.Handlers
{
    public class UpdateInvestmentHandler(
        IRepository<Investment> investmentRepository,
        IRepository<InvestmentStatus> investmentStatusRepository,
        IMapper mapper
    ) : IRequestHandler<UpdateInvestmentCommand, ResultDto<int>>
    {
        private readonly IRepository<Investment> _investmentRepository = investmentRepository;
        private readonly IRepository<InvestmentStatus> _investmentStatusRepository = investmentStatusRepository;
        private readonly IMapper _mapper = mapper; // O Mapper pode ser útil para DTOs de retorno

        public async Task<ResultDto<int>> Handle(UpdateInvestmentCommand request, CancellationToken cancellationToken)
        {
            // 1. Converter e validar o status recebido como string para o enum
            // O 'true' em Enum.TryParse o torna case-insensitive (mais robusto)
            if (!Enum.TryParse<ETransactionStatus>(request.Status, true, out var parsedStatusEnum))
            {
                // Se a string não corresponder a nenhum membro do enum, retorna um erro.
                throw new ArgumentException($"O status fornecido '{request.Status}' é inválido.");
            }

            // 2. Buscar o investimento existente no banco de dados
            var investment = await _investmentRepository.FindAsync(x => x.Id == request.Id);

            if (investment == null)
            {
                throw new InvalidOperationException($"Investimento com ID {request.Id} não foi encontrado.");
            }

            // 3. Atualizar as propriedades do investimento com os valores do command
            // Apenas atualizamos os campos que foram fornecidos (não são nulos)
            if (request.ConfirmedValue.HasValue)
                investment.ConfirmedValue = request.ConfirmedValue.Value;

            if (request.AssetValue.HasValue)
                investment.AssetValue = request.AssetValue.Value;

            if (!string.IsNullOrEmpty(request.ContractFile))
                investment.ContractFile = request.ContractFile;

            if (request.Active.HasValue)
                investment.Active = request.Active.Value;

            investment.UpdatedAt = DateTime.UtcNow;

            // O repositório do EF Core rastreia essas mudanças automaticamente.

            // 4. Criar o novo registro de histórico de status
            var newStatus = new InvestmentStatus
            {
                InvestmentId = investment.Id,
                Status = parsedStatusEnum, // Usamos o enum que foi convertido
                CreatedAt = DateTime.UtcNow
            };

            await _investmentStatusRepository.AddAsync(newStatus);

            // 5. Salvar o novo status para obter seu ID e depois atualizar o investimento
            //    (Seguindo o padrão do seu CreateHandler)

            // Primeiro, salvamos o status para que o BD gere seu ID
            await _investmentStatusRepository.SaveChangesAsync();

            // Agora, com o ID gerado, atualizamos a referência no investimento principal
            investment.StatusId = newStatus.Id;

            // Finalmente, salvamos as alterações no investimento
            await _investmentRepository.SaveChangesAsync();

            // 6. Retornar uma resposta de sucesso
            return ResultDto<int>.SuccessResult(investment.Id, "Investimento atualizado com sucesso!");
        }
    }
}
