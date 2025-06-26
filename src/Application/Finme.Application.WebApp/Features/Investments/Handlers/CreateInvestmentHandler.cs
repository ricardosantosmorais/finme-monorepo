using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Application.WebApp.Features.Investments.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Enums;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Operations.Handlers
{
    public class CreateInvestmentHandler(
            IRepository<Investment> investmentRepository,
            IRepository<InvestmentStatus> investmentStatusRepository,
            IMapper mapper
        ) : IRequestHandler<CreateInvestmentCommand, ResultDto<int>>
    {
        private readonly IRepository<Investment> _investmentRepository = investmentRepository;
        private readonly IRepository<InvestmentStatus> _investmentStatusRepository = investmentStatusRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<ResultDto<int>> Handle(CreateInvestmentCommand request, CancellationToken cancellationToken)
        {
            var investments = await _investmentRepository.Get(x => x.UserId == request.UserId && x.OperationId == request.OperationId && !x.DeletedAt.HasValue);
            Investment? investment = null; // Explicitly specify the type as nullable Investment  
            var canInvest = false;

            // Se houver investimentos, nessa lista, indica que o usuário possui investimentos nessa operação  
            if (investments != null && investments.Any())
            {
                investment = investments.FirstOrDefault();
                var status = await _investmentStatusRepository.FindAsync(x => x.Id == investment!.StatusId);
                if (status != null)
                {
                    if (status.Status == ETransactionStatus.RescissionByClient
                        && status.Status == ETransactionStatus.CancelledByClient
                        && status.Status == ETransactionStatus.CancelledManually
                        && status.Status == ETransactionStatus.ConfirmedValue)
                    {
                        canInvest = true;
                    }
                }
            }
            else
            {
                canInvest = true;
            }

            if (!canInvest)
            {
                return ResultDto<int>.SuccessResult(investment!.Id, "Investimento criada com sucesso!");
            }

            // Salvando o investimento  
            var newInvestment = _mapper.Map<Investment>(request);
            newInvestment.Active = true;
            newInvestment.CreatedAt = DateTime.UtcNow;

            await _investmentRepository.AddAsync(newInvestment);
            await _investmentRepository.SaveChangesAsync();

            var newStatus = new InvestmentStatus
            {
                InvestmentId = newInvestment.Id,
                Status = ETransactionStatus.Interest,
                Active = true,
                CreatedAt = DateTime.UtcNow
            };

            await _investmentStatusRepository.AddAsync(newStatus);
            await _investmentStatusRepository.SaveChangesAsync();

            newInvestment.StatusId = newStatus.Id;
            await _investmentRepository.SaveChangesAsync();

            return ResultDto<int>.SuccessResult(newInvestment.Id, "Investimento criada com sucesso!");
        }
    }
}
