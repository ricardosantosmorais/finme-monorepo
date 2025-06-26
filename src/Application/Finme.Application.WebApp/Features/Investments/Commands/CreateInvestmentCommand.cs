using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Investments.Commands
{
    public class CreateInvestmentCommand : IRequest<ResultDto<int>> // Supondo que você tenha um ResultDto padrão
    {
        public int OperationId { get; set; }
        public int UserId { get; set; }
        public decimal? CommitedValue { get; set; }
        public decimal? ConfirmedValue { get; set; }
        public decimal? AssetValue { get; set; }
    }
}
