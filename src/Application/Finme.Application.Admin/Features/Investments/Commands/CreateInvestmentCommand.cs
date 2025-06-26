using Finme.Application.Admin.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Features.Investments.Commands
{
    public class CreateInvestmentCommand : IRequest<ResultDto<int>> // Supondo que você tenha um ResultDto padrão
    {
        public int OperationId { get; set; }
        public int UserId { get; set; }
        public decimal CommitedValue { get; set; }
        public decimal? ConfirmedValue { get; set; }
        public decimal? AssetValue { get; set; }
    }
}
