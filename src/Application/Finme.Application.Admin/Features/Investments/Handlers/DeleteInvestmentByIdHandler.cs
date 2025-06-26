using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Investments.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Investments.Handlers
{
    public class DeleteInvestmentByIdHandler(
            IRepository<Investment> investmentRepository,
            IMapper mapper
        ) : IRequestHandler<DeleteInvestmentByIdCommand, ResultDto<int>>
    {
        private readonly IRepository<Investment> _investmentRepository = investmentRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<ResultDto<int>> Handle(DeleteInvestmentByIdCommand request, CancellationToken cancellationToken)
        {
            var investment = await _investmentRepository.GetByIdAsync(request.Id);
            if (investment == null)
            {
                throw new InvalidOperationException($"Investimento com ID {request.Id} não foi encontrado.");
            }
            investment.Active = false; // Desativando o investimento
            investment.DeletedAt = DateTime.UtcNow; // Marcando como deletado
            _investmentRepository.Update(investment);
            await _investmentRepository.SaveChangesAsync();
            return ResultDto<int>.SuccessResult(investment.Id, "Investimento excluído com sucesso!");
        }
    }
}
