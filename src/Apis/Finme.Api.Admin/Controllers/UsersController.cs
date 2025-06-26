using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.DTOs.Request;
using Finme.Application.Admin.Features.Operations.Commands;
using Finme.Application.Admin.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("all")]
        public async Task<IActionResult> All()
        {
            try
            {
                var response = await _mediator.Send(new GetAllUsersCommand {});
                return Ok(response);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetUser([FromBody] GetUserRequest request)
        {
            var response = await _mediator.Send(new GetUserByIdCommand
            {
                Id = request.Id
            }
            );
            return Ok(response);
        }

        [HttpPost("change")]
        public async Task<IActionResult> Change([FromBody] UserRequestDto request)
        {
            var response = await _mediator.Send(new ChangeUserCommand
            {
                Id = request.Id,
                Email = request.Email,
                Telephone = request.Telephone,
                FullName = request.FullName,
                Image = request.Image,
                DateOfBirth = request.DateOfBirth,
                Cpf = request.Cpf,
                DocumentNumber = request.DocumentNumber,
                Naturalness = request.Naturalness,
                Nationality = request.Nationality,
                Gender = request.Gender,
                MaritalStatus = request.MaritalStatus,
                CpfSpouse = request.CpfSpouse,
                FullNameSpouse = request.FullNameSpouse,
                BusinessName = request.BusinessName,
                Cnpj = request.Cnpj,
                CurrentlyWorks = request.CurrentlyWorks,
                Occupation = request.Occupation,
                FatherName = request.FatherName,
                MotherName = request.MotherName,
                MonthlyIncome = request.MonthlyIncome,
                FinancialApplications = request.FinancialApplications,
                OtherIncomes = request.OtherIncomes,
                RealEstate = request.RealEstate,
                RelatedPerson = request.RelatedPerson,
                PoliticallyExposed = request.PoliticallyExposed,
                TaxResidence = request.TaxResidence,
                Status = request.Status,
                Active = request.Active,
                Banks = request.Banks?.Select(b => new ChangeUserCommand.UserBank
                {
                    UserId = b.UserId,
                    Bank = b.Bank,
                    Agency = b.Agency,
                    CurrentAccount = b.CurrentAccount,
                    JoinAccount = b.JoinAccount,
                    CpfSpouse = b.CpfSpouse,
                    FullNameSpouse = b.FullNameSpouse
                }).ToList(), // Note que no Command é um único objeto, enquanto no DTO é um array

                Address = request.Address?.Select(a => new ChangeUserCommand.UserAddress
                {
                    UserId = a.UserId,
                    Cep = a.Cep,
                    Address = a.Address,
                    Number = a.Number,
                    Complement = a.Complement,
                    Neighborhood = a.Neighborhood,
                    City = a.City,
                    State = a.State
                }).ToList(),

                Documents = request.Documents?.Select(d => new ChangeUserCommand.UserDocument
                {
                    Id = d.Id,
                    UserId = d.UserId,
                    FileName = d.FileName,
                    DocumentType = d.DocumentType,
                    Type = d.Type,
                    Active = d.Active
                }).ToList(),

                Bookmarks = request.Bookmarks?.Select(b => new ChangeUserCommand.Bookmark
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    OperationId = b.OperationId,
                    Operation = b.Operation // assumindo que OperationDto é compatível
                }).ToList()
            }
            );
            return Ok(response);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordRequest request)
        {
            var response = await _mediator.Send(new ChangeUserPasswordCommand
            {
                Id = request.Id,
                Password = request.Password
            }
            );
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserRequest request)
        {
            var response = await _mediator.Send(new DeleteUserCommand
            {
                Id = request.Id
            });
            return Ok(response);
        }
    }
}
