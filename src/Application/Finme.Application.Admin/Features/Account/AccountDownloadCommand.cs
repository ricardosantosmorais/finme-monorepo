using Finme.Application.Admin.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Application.Admin.Features.Account.Commands
{
    public class AccountDownloadCommand : IRequest<DownloadDocumentResponse>
    {
        public int UserId { get; set; }
        public int DocumentId { get; set; }
    }
}
