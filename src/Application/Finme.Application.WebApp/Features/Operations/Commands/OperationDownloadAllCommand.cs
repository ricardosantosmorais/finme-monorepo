using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Operations.Commands
{
    public class OperationDownloadAllCommand : IRequest<DownloadDocumentResponse>
    {
        public int OperationId { get; set; }
    }
}
