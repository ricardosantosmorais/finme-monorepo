using Finme.Application.WebApp.DTOs;
using Finme.Application.WebApp.DTOs.Request;
using Finme.Application.WebApp.Features.Account.Commands;
using Finme.Application.WebApp.Features.Operations.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OperationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("all")]
        public async Task<IActionResult> All([FromBody] OperationRequestDto request)
        {
            var response = await _mediator.Send(new OperationAllCommand 
            { 
                Category = request.Category,
                Name = request.Name,
                Type = request.Type,
                Status = request.Status,
            }
            );
            return Ok(response);
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetOperation([FromBody] GetOperationRequest request)
        {
            var response = await _mediator.Send(new OperationCommand
            {
                Alias = request.Alias
            }
            );
            return Ok(response);
        }

        [HttpPost("download")]
        public async Task<IActionResult> Download([FromBody] OperationDownloadDto document)
        {
            var response = await _mediator.Send(new OperationDownloadCommand
            {
                OperationId = document.operationId,
                DocumentId = document.documentId
            });
            return File(response.FileStream, response.ContentType, response.FileName);
        }

        [HttpGet("download/{operationId}/{documentId}")]
        public async Task<IActionResult> DownloadFile(int operationId, int documentId)
        {
            var response = await _mediator.Send(new OperationDownloadCommand
            {
                OperationId = operationId,
                DocumentId = documentId
            });
            return File(response.FileStream, response.ContentType, response.FileName);
        }

        [HttpPost("download-all")]
        public async Task<IActionResult> DownloadAll([FromBody] OperationDownloadAllDto document)
        {
            var response = await _mediator.Send(new OperationDownloadAllCommand
            {
                OperationId = document.operationId,
            });
            return File(response.FileStream, response.ContentType, response.FileName);
        }        
    }
}
