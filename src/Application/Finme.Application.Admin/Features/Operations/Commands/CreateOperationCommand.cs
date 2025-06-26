using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Operations.Commands
{
    public class CreateOperationCommand : IRequest<ResultDto<int>> // Supondo que você tenha um ResultDto padrão
    {
        public string Name { get; set; }
        public string SocialName { get; set; }
        public string Cnpj { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal TargetProfitability { get; set; }
        public decimal MinimumInvestment { get; set; }
        public decimal MaximumInvestment { get; set; }
        public decimal InvestmentTarget { get; set; }
        public DateTime FinalDate { get; set; }
        public string InvestmentTerms { get; set; }
        public string? Image { get; set; }
        public string? Background { get; set; }
        public int? Term { get; set; }
        public string? Payment { get; set; }
        public decimal? QuoteValue { get; set; }
        public string? Type { get; set; }
        public decimal? Value { get; set; }
        public string? Category { get; set; }
        public string? Modality { get; set; }
        public string? Status { get; set; }
        public string? PitchTitle { get; set; }
        public string? PitchText { get; set; }
        public DateTime? PitchDate { get; set; }
        public string? PitchImage { get; set; }

    }
}
