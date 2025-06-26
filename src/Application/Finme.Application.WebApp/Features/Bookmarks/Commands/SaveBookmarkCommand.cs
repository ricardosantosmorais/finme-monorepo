using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Bookmarks.Commands
{
    public class SaveBookmarkCommand : IRequest<BookmarkDto>
    {
        public int UserId { get; set; }
        public int OperationId { get; set; }
    }
}
