using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Operations.Commands
{
    public class OperationCommand : IRequest<OperationDto>
    {
        public int Id { get; set; }
    }
}
