using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _mediator.Send(new LoginCommand { Email = dto.Email, Password = dto.Password });
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountDto dto)
        {
            var userId = await _mediator.Send(new RegisterCommand
            {
                Email = dto.Email,
                Password = dto.Password,
                Telephone = dto.Telephone,
                Cpf = dto.Cpf,
                FullName = dto.FullName
            });
            return Ok(new { userId, dto.Telephone, dto.FullName });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotRequestDto dto)
        {
            var result = await _mediator.Send(new ForgotCommand
            {
                Key = dto.Key,
                Channel = dto.Channel,
            });
            return Ok(result);
        }

        [HttpPost("confirm-verification-code")]
        public async Task<IActionResult> ConfirmVerificationCode([FromBody] ConfirmVerificationCodeDto dto)
        {
            var result = await _mediator.Send(new ConfirmVerificationCodeCommand
            {
                UserId = dto.UserId,
                Code = dto.Code
            });
            return Ok(result);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordRequestDto dto)
        {
            var result = await _mediator.Send(new ChangePasswordCommand
            {
                UserId = dto.UserId,
                Code = dto.Code,
                Password = dto.Password
            });
            return Ok(result);
        }
    }
}
