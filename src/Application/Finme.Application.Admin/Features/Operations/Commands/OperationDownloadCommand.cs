using Finme.Application.Admin.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Features.Operations.Commands
{
    public class OperationDownloadCommand : IRequest<DownloadDocumentResponse>
    {
        public int OperationId { get; set; }
        public int DocumentId { get; set; }
    }
}
