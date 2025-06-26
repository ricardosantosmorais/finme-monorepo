using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.DTOs.Request;
using Finme.Application.Admin.Features.Operations.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.Admin.Controllers
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
            try
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
            catch
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Cria uma nova operação.
        /// </summary>
        /// <returns>O ID da nova operação.</returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateOperationRequestDto request)
        {
            // O AutoMapper pode ser usado aqui também, se preferir.
            var command = new CreateOperationCommand
            {
                Name = request.Name,
                SocialName = request.SocialName,
                Cnpj = request.Cnpj,
                Description = request.Description,
                Date = request.Date,
                TargetProfitability = request.TargetProfitability,
                MinimumInvestment = request.MinimumInvestment,
                MaximumInvestment = request.MaximumInvestment,
                InvestmentTarget = request.InvestmentTarget,
                FinalDate = request.FinalDate,
                InvestmentTerms = request.InvestmentTerms,
                Term = request.Term,
                Payment = request.Payment,
                QuoteValue = request.QuoteValue,
                Type = request.Type,
                Value = request.Value,
                Category = request.Category,
                Modality = request.Modality,
                Status = request.Status,
                Image = request.Image,
                Background = request.Background,
                PitchDate = request.PitchDate,
                PitchImage = request.PitchImage,
                PitchText = request.PitchText,
                PitchTitle = request.PitchTitle
            };

            var response = await _mediator.Send(command);

            if (response.Success)
            {
                // Retorna 201 Created com a localização do novo recurso e o ID no corpo.
                // "GetOperation" deve ser o nome da sua action de busca por ID.
                return CreatedAtAction(nameof(GetOperation), new { id = response.Data }, response);
            }

            // Retorna 400 Bad Request com a lista de erros.
            return BadRequest(response);
        }

        /// <summary>
        /// Atualiza uma operação existente.
        /// </summary>
        [HttpPost("change")]
        [ProducesResponseType(typeof(ResultDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateOperationRequestDto request)
        {
            // O AutoMapper pode ser usado aqui também.
            var command = new UpdateOperationCommand
            {
                Id = request.Id,
                SocialName = request.SocialName,
                Name = request.Name,
                Cnpj = request.Cnpj,
                Description = request.Description,
                Date = request.Date,
                TargetProfitability = request.TargetProfitability,
                MinimumInvestment = request.MinimumInvestment,
                MaximumInvestment = request.MaximumInvestment,
                InvestmentTarget = request.InvestmentTarget,
                FinalDate = request.FinalDate,
                InvestmentTerms = request.InvestmentTerms,
                Term = request.Term,
                Payment = request.Payment,
                QuoteValue = request.QuoteValue,
                Type = request.Type,
                Value = request.Value,
                Category = request.Category,
                Modality = request.Modality,
                Status = request.Status,
                PitchDate = request.PitchDate,
                PitchText = request.PitchText,
                PitchTitle = request.PitchTitle
            };

            // Supondo que o Update handler retorna ResultDto<bool> ou ResultDto<object>
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                // Retorna 200 OK com a mensagem de sucesso.
                return Ok(response);
            }

            // Retorna 400 Bad Request com os erros (ex: "Operação não encontrada" ou erros de validação).
            return BadRequest(response);
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetOperation([FromBody] GetOperationRequest request)
        {
            try
            {
                var response = await _mediator.Send(new OperationCommand
                {
                    Id = request.Id
                }
                );
                return Ok(response);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocuments(
            [FromForm(Name = "file")] IFormFile file,
            [FromForm(Name = "operationId")] int operationId,
            [FromForm(Name = "contentType")] string contentType,
            [FromForm(Name = "fieldName")] string? fieldName,
            [FromForm(Name = "documentType")] string documentType)
        {
            if (file == null)
            {
                return BadRequest("Nenhum arquivo enviado.");
            }

            var response = await _mediator.Send(new OperationUploadCommand
            {
                OperationId = operationId,
                File = file,
                ContentType = contentType,
                DocumentType = documentType,
                FieldName = fieldName
            });
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
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Client, NoStore = false, VaryByHeader = "Accept")]
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
