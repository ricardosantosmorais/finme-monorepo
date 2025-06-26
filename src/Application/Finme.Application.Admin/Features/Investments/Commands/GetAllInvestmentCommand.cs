using Finme.Application.Admin.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Features.Investments.Commands
{
    public class GetAllInvestmentCommand : IRequest<List<InvestmentDto>>
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? OperationId { get; set; }
    }
}
