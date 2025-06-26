using Finme.Application.WebApp.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class AccountDocumentsCommand : IRequest<UserResponseDto>
    {
        public int Id { get; set; }
        public IFormFileCollection Files { get; set; }
        public string[] ContentTypes { get; set; }
        public string[] DocumentTypes { get; set; }
    }
}
