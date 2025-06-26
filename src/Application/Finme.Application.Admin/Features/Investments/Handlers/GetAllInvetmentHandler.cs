using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Investments.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Investments.Handlers
{
    public class GetAllInvetmentHandler(
            IRepository<Investment> investmentRepository,
            IRepository<InvestmentStatus> investmentStatusRepository,
            IMapper mapper
        ) : IRequestHandler<GetAllInvestmentCommand, List<InvestmentDto>>
    {
        private readonly IRepository<Investment> _investmentRepository = investmentRepository;
        private readonly IRepository<InvestmentStatus> _investmentStatusRepository = investmentStatusRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<List<InvestmentDto>> Handle(GetAllInvestmentCommand request, CancellationToken cancellationToken)
        {
            var investments = await _investmentRepository.Get(x => !x.DeletedAt.HasValue);
            var response = new List<InvestmentDto>();
            if (investments != null)
            {
                foreach (var investment in investments)
                {
                    var investmentDto = _mapper.Map<InvestmentDto>(investment);
                    // Fetch and map the status
                    var status = await _investmentStatusRepository.FindAsync(x => x.Id == investment.StatusId);
                    if (status != null)
                    {
                        investmentDto.InvestmentStatuses = [_mapper.Map<InvestmentStatusDto>(status)];
                    }
                    response.Add(investmentDto);
                }
            }
            return response;
        }
    }
}
