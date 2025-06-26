using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Operations.Commands
{
    public class OperationDownloadCommand : IRequest<DownloadDocumentResponse>
    {
        public int OperationId { get; set; }
        public int DocumentId { get; set; }
    }
}
