using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Entities
{
    public class Operation : Base
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string SocialName { get; set; }
        public string Cnpj { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string? Image { get; set; }
        public string? Background { get; set; }
        public decimal? TargetProfitability { get; set; } // Rentabilidade Alvo
        public decimal? MinimumInvestment { get; set; } // Investimento Mínimo
        public decimal? MaximumInvestment { get; set; } // Investimento Máximo
        public decimal? InvestmentTarget { get; set; } // Alvo de Investimento
        public DateTime? FinalDate { get; set; } // Data Final
        public string? InvestmentTerms { get; set; } // Termos do Investimento
        public int? Term { get; set; } // Prazo
        public string? Payment { get; set; } // Pagamento
        public decimal? QuoteValue { get; set; } // Cota
        public string? Type { get; set; }
        public decimal? Value { get; set; }
        public string? Category { get; set; }

        public string? ContractFile { get; set; }
        public decimal? ShareValue { get; set; } // Valor da Cota
        public decimal? Participation { get; set; } // Participação
        public string? Modality { get; set; } // Modalidade
        public string? Status { get; set; } // Status
        public decimal? AmountCollected { get; set; } // Valor Captado
        public string? ProjectedPayments { get; set; } // Pagamentos Projetados
        public int? Investors { get; set; }
        public string? PitchTitle { get; set; }
        public string? PitchText { get; set; }
        public DateTime? PitchDate { get; set; }
        public string? PitchImage { get; set; }

        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<OperationComment>? Comments { get; set; }
        public List<Investment>? Investments { get; set; }
        public List<OperationFile>? Files { get; set; }

    }
}
