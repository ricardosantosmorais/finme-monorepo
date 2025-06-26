using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Investments.Commands
{
    public class DeleteInvestmentByIdCommand : IRequest<ResultDto<int>>
    {
        public int Id { get; set; }
    }
}
