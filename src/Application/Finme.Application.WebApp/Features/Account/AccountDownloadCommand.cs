using Finme.Application.WebApp.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class AccountDownloadCommand : IRequest<DownloadDocumentResponse>
    {
        public int UserId { get; set; }
        public int DocumentId { get; set; }
    }
}
