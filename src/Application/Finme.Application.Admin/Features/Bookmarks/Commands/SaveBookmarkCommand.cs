using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Bookmarks.Commands
{
    public class SaveBookmarkCommand : IRequest<BookmarkDto>
    {
        public int UserId { get; set; }
        public int OperationId { get; set; }
    }
}
