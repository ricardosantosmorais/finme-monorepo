using AutoMapper;
using Finme.Application.Admin.Features.Bookmarks.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Bookmarks.Handlers
{
    public class DeleteBookmarkHandler(
        IRepository<Bookmark> bookmarkRepository,
            IMapper mapper) : IRequestHandler<DeleteBookmarkCommand, bool>

    {
        private readonly IRepository<Bookmark> _bookmarkRepository = bookmarkRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> Handle(DeleteBookmarkCommand request, CancellationToken cancellationToken)
        {
            var bookmark = await _bookmarkRepository.GetByIdAsync(request.Id);

            if (bookmark == null)
            {
                return false; // Bookmark not found
            }
            _bookmarkRepository.Delete(bookmark);
            await _bookmarkRepository.SaveChangesAsync();
            return true;
        }
    }
}
