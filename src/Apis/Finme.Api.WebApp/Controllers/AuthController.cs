using Finme.Application.WebApp.DTOs;
using Finme.Application.WebApp.Features.Account.Commands;
using Finme.Application.WebApp.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var response = await _mediator.Send(new LoginCommand { Email = dto.Email, Password = dto.Password });
                var user = await _mediator.Send(new AccountCommand
                {
                    Id = Convert.ToInt32(response.Id),                    
                });
                return Ok(user);
            }
            catch
            {
                return Unauthorized();
            }
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
            if (userId > 0)
            {
                await _mediator.Send(new SendVerificationCodeCommand
                {
                    UserId = userId,
                    Channel = "SMS"
                });
            }
            return Ok(new { userId, dto.Telephone, dto.FullName });
        }

        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] SendVerificationCodeDto dto)
        {
            await _mediator.Send(new SendVerificationCodeCommand
            {
                UserId = dto.UserId,
                Channel = dto.Channel
            });
            return Ok(new { dto.UserId });
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

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordRequestDto dto)
        {
            var result = await _mediator.Send(new ChangePasswordCommand
            {
                UserId = dto.UserId,
                Password = dto.Password
            });
            return Ok(result);
        }
    }
}
