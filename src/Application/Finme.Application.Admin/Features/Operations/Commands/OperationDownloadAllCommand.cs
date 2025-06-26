using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Operations.Commands
{
    public class OperationDownloadAllCommand : IRequest<DownloadDocumentResponse>
    {
        public int OperationId { get; set; }
    }
}
