using Finme.Application.WebApp.DTOs.Request;
using Finme.Application.WebApp.Features.Investments.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvestmentController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("save")] //Salva como interest
        public async Task<IActionResult> Save([FromBody] SaveInvestmentRequest request)
        {
            var command = new CreateInvestmentCommand
            {
                OperationId = request.OperationId,
                UserId = Convert.ToInt32(User.Identity.Name),
                CommitedValue = request.CommitedValue,
                ConfirmedValue = request.ConfirmedValue,
                AssetValue = request.AssetValue,
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("report-value")]
        public async Task<IActionResult> ReportValue([FromBody] UpdateInvestmentRequest request)
        {
            var response = await _mediator.Send(new UpdateInvestmentValueCommand
            {
                InvestmentId = request.InvestmentId,
                UserId = Convert.ToInt32(User.Identity.Name),
                OperationId = request.OperationId,
                CommitedValue = request.CommitedValue!.Value,
            });
            return Ok(response);
        }

        [HttpPost("contract-accept")]
        public async Task<IActionResult> ContractAccept([FromBody] UpdateInvestmentRequest request)
        {
            var response = await _mediator.Send(new UpdateInvestmentContractAcceptCommand
            {
                InvestmentId = request.InvestmentId,
                UserId = Convert.ToInt32(User.Identity.Name),
                OperationId = request.OperationId
            });
            return Ok(response);
        }

        [HttpPost("resend-code")]
        public async Task<IActionResult> ResendCode([FromBody] ResendCodeInvestmentRequest request)
        {
            var response = await _mediator.Send(new SendCodeInvestmentCommand
            {
                InvestmentId = request.InvestmentId,
                UserId = Convert.ToInt32(User.Identity.Name),
                OperationId = request.OperationId,
                Channel = request.Channel // "SMS" ou "Email"
            });
            return Ok(response);
        }

        [HttpPost("contract")]
        public async Task<IActionResult> Contract([FromBody] GetInvestmentContractRequest request)
        {
            var response = await _mediator.Send(new InvestmentContractCommand
            {
                InvestmentId = request.InvestmentId,
                UserId = Convert.ToInt32(User.Identity.Name),
                OperationId = request.OperationId,
            });
            return File(response.FileStream, response.ContentType, response.FileName);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmInvestment([FromBody] ConfirmInvestmentRequest request)
        {
            var response = await _mediator.Send(new ConfirmInvestmentCommand
            {
                InvestmentId = request.InvestmentId,
                UserId = Convert.ToInt32(User.Identity.Name),
                OperationId = request.OperationId,
                Code = request.Code
            });
            return Ok(response);
        }
    }
}
