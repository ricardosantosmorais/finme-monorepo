using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Operations.Commands
{
    public class OperationAllCommand : IRequest<OperationDto[]>
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
    }
}
