using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Operations.Commands
{
    public class OperationCommand : IRequest<OperationDto>
    {
        public string Alias { get; set; }
    }
}
