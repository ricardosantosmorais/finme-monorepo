using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Investments.Commands
{
    public class SendCodeInvestmentCommand : IRequest<ResultDto<bool>>
    {
        public int InvestmentId { get; set; }
        public int OperationId { get; set; }
        public int UserId { get; set; }
        public string Channel { get; set; }
    }
}
