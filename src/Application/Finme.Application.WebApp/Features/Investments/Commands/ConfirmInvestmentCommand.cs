using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Investments.Commands
{
    public class ConfirmInvestmentCommand : IRequest<ResultDto<int>>
    {
        public int InvestmentId { get; set; }
        public int OperationId { get; set; }
        public string Code { get; set; }
        public int UserId { get; set; }
    }
}
