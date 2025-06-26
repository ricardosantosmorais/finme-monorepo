using Finme.Application.WebApp.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.Features.Operations.Commands
{
    public class OperationContractCommand : IRequest<DownloadDocumentResponse>
    {
        public int OperationId { get; set; }
        public int UserId { get; set; }
    }
}
