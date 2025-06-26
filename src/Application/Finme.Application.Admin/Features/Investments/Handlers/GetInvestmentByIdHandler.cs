using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Investments.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Features.Investments.Handlers
{
    public class GetInvestmentByIdHandler(
            IRepository<Investment> investmentRepository,
            IMapper mapper
        ) : IRequestHandler<GetInvestmentByIdCommand, InvestmentDto>
    {
        private readonly IRepository<Investment> _investmentRepository = investmentRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<InvestmentDto> Handle(GetInvestmentByIdCommand request, CancellationToken cancellationToken)
        {
            var investment = await _investmentRepository.GetInvestmentsWithDependenciesAsync(x => x.Id == request.Id);
            return _mapper.Map<InvestmentDto>(investment);
        }
    }
}
