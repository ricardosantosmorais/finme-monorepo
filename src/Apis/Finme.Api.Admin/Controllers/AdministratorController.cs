using Finme.Application.Admin.DTOs.Request;
using Finme.Application.Admin.Features.Administrators.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdministratorController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("all")]
        public async Task<IActionResult> All()
        {
            var response = await _mediator.Send(new GetAllAdministratorsCommand {});
            return Ok(response);
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetAdministrator([FromBody] GetAdministratorRequest request)
        {
            var response = await _mediator.Send(new GetAdministratorByIdCommand
            {
                Id = request.Id
            }
            );
            return Ok(response);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] SaveAdministratorRequest request)
        {
            var response = await _mediator.Send(new SaveAdministratorCommand
            {
                Cpf = request.Cpf,
                Email = request.Email,
                FullName = request.FullName,
                Image = request.Image,
                Password = request.Password,
                Telephone = request.Telephone,
                Active = request.Active,
            }
            );
            return Ok(response);
        }

        [HttpPost("change")]
        public async Task<IActionResult> Change([FromBody] ChangeAdministratorRequest request)
        {
            var response = await _mediator.Send(new ChangeAdministratorCommand
            {
                Id = request.Id,
                Cpf = request.Cpf,
                Email = request.Email,
                FullName = request.FullName,
                Image = request.Image,
                Telephone = request.Telephone,
                Status = request.Status,
                Active = request.Active,
            }
            );
            return Ok(response);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeAdministratorPasswordRequest request)
        {
            var response = await _mediator.Send(new ChangeAdministratorPasswordCommand
            {
                Id = request.Id,
                Password = request.Password
            }
            );
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteAdministratorRequest request)
        {
            var response = await _mediator.Send(new DeleteAdministratorCommand
            {
                Id = request.Id
            });
            return Ok(response);
        }
    }
}
