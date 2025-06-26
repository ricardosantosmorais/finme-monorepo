using Finme.Application.Admin.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Features.Bookmarks.Commands
{
    public class DeleteBookmarkCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
