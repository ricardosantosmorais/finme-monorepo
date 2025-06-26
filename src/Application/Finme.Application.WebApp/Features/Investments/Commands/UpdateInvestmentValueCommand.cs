using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Investments.Commands
{
    public class UpdateInvestmentValueCommand : IRequest<ResultDto<int>>
    {
        public int InvestmentId { get; set; }
        public int OperationId { get; set; }
        public decimal CommitedValue { get; set; }
        public int UserId { get; set; }
    }
}
