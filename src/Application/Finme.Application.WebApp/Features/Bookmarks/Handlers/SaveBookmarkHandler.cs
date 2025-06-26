using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Application.WebApp.Features.Bookmarks.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Bookmarks.Handlers
{
    public class SaveBookmarkHandler(
        IRepository<Bookmark> bookmarkRepository,
            IMapper mapper) : IRequestHandler<SaveBookmarkCommand, BookmarkDto>

    {
        private readonly IRepository<Bookmark> _bookmarkRepository = bookmarkRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BookmarkDto> Handle(SaveBookmarkCommand request, CancellationToken cancellationToken)
        {
            var bookmark = new Bookmark
            {
                UserId = request.UserId,
                OperationId = request.OperationId,
                CreatedAt = DateTime.UtcNow,
                Active = true
            };

            await _bookmarkRepository.AddAsync(bookmark);
            await _bookmarkRepository.SaveChangesAsync();
            return _mapper.Map<BookmarkDto>(bookmark);
        }
    }
}
