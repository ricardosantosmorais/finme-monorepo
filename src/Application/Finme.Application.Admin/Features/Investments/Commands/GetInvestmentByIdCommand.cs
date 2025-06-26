using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Investments.Commands
{
    public class GetInvestmentByIdCommand: IRequest<InvestmentDto>
    {
        public int Id { get; set; }
    }
}
