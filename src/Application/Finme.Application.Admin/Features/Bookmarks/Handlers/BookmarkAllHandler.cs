using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Bookmarks.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Bookmarks.Handlers
{
    public class BookmarkAllHandler(
        IRepository<Bookmark> bookmarkRepository,
            IMapper mapper) : IRequestHandler<BookmarkAllCommand, List<BookmarkDto>>

    {
        private readonly IRepository<Bookmark> _bookmarkRepository = bookmarkRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<BookmarkDto>> Handle(BookmarkAllCommand request, CancellationToken cancellationToken)
        {
          
            var bookmarks = await _bookmarkRepository.GetBookmarksWithDependenciesAsync(x => x.UserId == request.UserId);
            return _mapper.Map<List<BookmarkDto>>(bookmarks);
        }
    }
}
