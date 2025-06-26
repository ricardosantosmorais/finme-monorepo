using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs
{
    public class OperationDto
    {
        public string Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string SocialName { get; set; }
        public string Cnpj { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public string Background { get; set; }
        public decimal TargetProfitability { get; set; }
        public decimal MinimumInvestment { get; set; }
        public decimal MaximumInvestment { get; set; }
        public decimal InvestmentTarget { get; set; }
        public DateTime FinalDate { get; set; }
        public string InvestmentTerms { get; set; }
        public int Term { get; set; }
        public string Payment { get; set; }
        public decimal QuoteValue { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
        public string Category { get; set; }
        public string? ContractFile { get; set; }
        public decimal? ShareValue { get; set; }
        public decimal? Participation { get; set; }
        public string? Modality { get; set; }
        public string? Status { get; set; }
        public decimal? AmountCollected { get; set; }
        public string? ProjectedPayments { get; set; }
        public int? Investors { get; set; }
        public string? PitchTitle { get; set; }
        public string? PitchText { get; set; }
        public DateTime? PitchDate { get; set; }
        public string? PitchImage { get; set; }
        public List<OperationCommentDto>? Comments { get; set; }
        public List<OperationFileDto>? Files { get; set; }
    }

    public class OperationCommentDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public string Date { get; set; }
        public int Likes { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public UserDto User { get; set; }
    }

    public class OperationFileDto
    {
        public string Id { get; set; }
        public int OperationId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string File { get; set; }
    }
}
