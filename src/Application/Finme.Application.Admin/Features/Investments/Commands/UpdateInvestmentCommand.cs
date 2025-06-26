using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Investments.Commands
{
    // Implemente IRequest para que o MediatR possa processá-lo
    public class UpdateInvestmentCommand : IRequest<ResultDto<int>>
    {
        public int Id { get; set; } // ID do investimento a ser atualizado

        // As propriedades abaixo são opcionais na atualização
        public decimal? ConfirmedValue { get; set; }
        public decimal? AssetValue { get; set; }
        public string? ContractFile { get; set; }
        public bool? Active { get; set; }

        // O novo status vem como string do frontend/API
        public string Status { get; set; }
    }
}
