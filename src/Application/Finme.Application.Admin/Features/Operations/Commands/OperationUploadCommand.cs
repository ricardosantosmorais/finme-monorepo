using Finme.Application.Admin.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Finme.Application.Admin.Features.Operations.Commands
{
    public class OperationUploadCommand : IRequest<ResultDto<int>>
    {
        public int OperationId { get; set; }
        public IFormFile File { get; set; }
        public string ContentType { get; set; }
        public string DocumentType { get; set; }
        public string? FieldName { get; set; }
    }
}
