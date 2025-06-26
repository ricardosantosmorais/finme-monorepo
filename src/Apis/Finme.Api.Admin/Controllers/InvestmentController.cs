using Finme.Application.Admin.DTOs.Request;
using Finme.Application.Admin.Features.Investments.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvestmentController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("all")]
        public async Task<IActionResult> All()
        {
            var response = await _mediator.Send(new GetAllInvestmentCommand { });
            return Ok(response);
        }

        [HttpPost("get")]
        public async Task<IActionResult> Get([FromBody] GetInvestmentRequest request)
        {
            var command = new GetInvestmentByIdCommand { Id = request.Id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] SaveInvestmentRequest request)
        {
            var command = new CreateInvestmentCommand
            {
                OperationId = request.OperationId,
                UserId = request.UserId,
                CommitedValue = request.CommitedValue,
                ConfirmedValue = request.ConfirmedValue,
                AssetValue = request.AssetValue,
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("change")]
        public async Task<IActionResult> Change([FromBody] UpdateInvestmentRequest request)
        {
            var command = new UpdateInvestmentCommand
            {
                Id = request.Id,
                ConfirmedValue = request.ConfirmedValue,
                AssetValue = request.AssetValue,
                ContractFile = request.ContractFile,
                Status = request.Status,
                Active = request.Active
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteInvestmentRequest request)
        {
            var command = new DeleteInvestmentByIdCommand { Id = request.Id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
