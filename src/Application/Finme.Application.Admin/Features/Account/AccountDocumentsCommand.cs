using Finme.Application.Admin.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Finme.Application.Admin.Features.Account.Commands
{
    public class AccountDocumentsCommand : IRequest<UserDto>
    {
        public int Id { get; set; }
        public IFormFileCollection Files { get; set; }
        public string[] ContentTypes { get; set; }
        public string[] DocumentTypes { get; set; }
    }
}
