using Finme.Application.WebApp.DTOs;
using Finme.Application.WebApp.Features.Account.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("profile")]
        public async Task<IActionResult> Account()
        {
            try
            {
                var response = await _mediator.Send(new AccountCommand { Id = Convert.ToInt32(User.Identity.Name) });
                return Ok(response);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost("persona")]
        public async Task<IActionResult> SavePersona([FromBody] UserRequestDto dto)
        {
            var response = await _mediator.Send(new AccountPersonaCommand
            {
                UserId = Convert.ToInt32(User.Identity.Name),
                CpfSpouse = dto.CpfSpouse,
                FullNameSpouse = dto.FullNameSpouse,
                DateOfBirth = dto.DateOfBirth,
                DocumentNumber = dto.DocumentNumber,
                Gender = dto.Gender,
                MaritalStatus = dto.MaritalStatus,
                Nationality = dto.Nationality,
                Naturalness = dto.Naturalness
            });
            return Ok(response);
        }

        [HttpPost("bank")]
        public async Task<IActionResult> SaveBank([FromBody] UserBankDto dto)
        {
            var response = await _mediator.Send(new AccountBankCommand
            {
                UserId = Convert.ToInt32(User.Identity.Name),
                CpfSpouse = dto.CpfSpouse,
                FullNameSpouse = dto.FullNameSpouse,
                Agency = dto.Agency,
                Bank = dto.Bank,
                CurrentAccount = dto.CurrentAccount,
                JoinAccount = dto.JoinAccount
            });
            return Ok(response);
        }

        [HttpPost("additional")]
        public async Task<IActionResult> SaveAdditional([FromBody] UserAdditionalDto dto)
        {
            var response = await _mediator.Send(new AccountAdditionalCommand
            {
                UserId = Convert.ToInt32(User.Identity.Name),
                Address = dto.Address,
                BusinessName = dto.BusinessName,
                Cnpj = dto.Cnpj,
                CurrentlyWorks = dto.CurrentlyWorks,
                FatherName = dto.FatherName,
                FinancialApplications = dto.FinancialApplications,
                MonthlyIncome = dto.MonthlyIncome,
                MotherName = dto.MotherName,
                Occupation = dto.Occupation,
                OtherIncomes = dto.OtherIncomes,
                PoliticallyExposed = dto.PoliticallyExposed,
                RelatedPerson = dto.RelatedPerson,
                RealEstate = dto.RealEstate,
                TaxResidence = dto.TaxResidence
            });
            return Ok(response);
        }

        [HttpPost("change-email")]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequestDto dto)
        {
            var response = await _mediator.Send(new ChangeEmailCommand
            {
                UserId = Convert.ToInt32(User.Identity.Name),
                Email = dto.Email,
                Code = dto.Code
            });
            return Ok(response);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto dto)
        {
            var response = await _mediator.Send(new AccountChangePasswordCommand
            {
                UserId = Convert.ToInt32(User.Identity.Name),
                Password = dto.Password,
                ConfirmPassword = dto.ConfirmPassword
            });
            return Ok(response);
        }

        [HttpPost("change-phone")]
        public async Task<IActionResult> ChangePhone([FromBody] ChangeTelephoneRequestDto dto)
        {
            var response = await _mediator.Send(new ChangeTelephoneCommand
            {
                UserId = Convert.ToInt32(User.Identity.Name),
                Phone = dto.Phone,
                Code = dto.Code
            });
            return Ok(response);
        }

        [HttpPost("send-code-sms")]
        public async Task<IActionResult> SendCodeSms([FromBody] ChangeTelephoneRequestDto dto)
        {
            await _mediator.Send(new AccountSendVerificationCodeCommand
            {
                UserId = Convert.ToInt32(User.Identity.Name),
                Phone = dto.Phone,
                Channel = "SMS"
            });
            return Ok(new { userId = Convert.ToInt32(User.Identity.Name) });
        }

        [HttpPost("send-code-email")]
        public async Task<IActionResult> SendCodeEmail([FromBody] ChangeEmailRequestDto dto)
        {
            await _mediator.Send(new AccountSendVerificationCodeCommand
            {
                UserId = Convert.ToInt32(User.Identity.Name),
                Email = dto.Email,
                Channel = "Email"
            });
            return Ok(new { userId = Convert.ToInt32(User.Identity.Name) });
        }

        [HttpPost("documents")]
        public async Task<IActionResult> UploadDocuments(
            [FromForm(Name = "files")] IFormFileCollection files,
            [FromForm(Name = "contentTypes")] string[] contentTypes,
            [FromForm(Name = "documentTypes")] string[] documentTypes)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("Nenhum arquivo enviado.");
            }

            var response = await _mediator.Send(new AccountDocumentsCommand
            {
                Id = Convert.ToInt32(User.Identity.Name),
                Files = files,
                ContentTypes = contentTypes,
                DocumentTypes = documentTypes
            });
            return Ok(response);
        }

        [HttpPost("download")]
        public async Task<IActionResult> Download([FromBody] DownloadDocumentDto document)
        {
            var response = await _mediator.Send(new AccountDownloadCommand
            {
                UserId = Convert.ToInt32(User.Identity.Name),
                DocumentId = document.documentId
            });
            return File(response.FileStream, response.ContentType, response.FileName);
        }

        [HttpPost("photo")]
        public async Task<IActionResult> UploadPhoto(
            [FromForm(Name = "files")] IFormFileCollection files,
            [FromForm(Name = "contentTypes")] string[] contentTypes)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("Nenhum arquivo enviado.");
            }

            var response = await _mediator.Send(new AccountPhotoCommand
            {
                Id = Convert.ToInt32(User.Identity.Name),
                Files = files,
                ContentTypes = contentTypes
            });
            return Ok(response);
        }

        [HttpGet("photo/{documentId}")]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Client, NoStore = false, VaryByHeader = "Accept")]
        public async Task<IActionResult> GetPhoto(int documentId)
        {
            var response = await _mediator.Send(new AccountDownloadCommand
            {
                UserId = Convert.ToInt32(User.Identity.Name),
                DocumentId = documentId
            });

            // Adiciona cabeçalho de cache
            Response.Headers.Remove("Content-Disposition");
            return File(response.FileStream, response.ContentType, response.FileName, enableRangeProcessing: true);
        }
    }
}
