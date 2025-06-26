using Finme.Application.Admin.DTOs.Request;
using Finme.Application.Admin.Features.Bookmarks.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookmarkController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("all")]
        public async Task<IActionResult> All()
        {
            try
            {
                var response = await _mediator.Send(new BookmarkAllCommand 
                { 
                    UserId = Convert.ToInt32(User.Identity.Name)
                }
                );
                return Ok(response);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] SaveBookmarkRequest request)
        {
            try
            {
                var response = await _mediator.Send(new SaveBookmarkCommand
                {
                    UserId = Convert.ToInt32(User.Identity.Name),
                    OperationId = request.OperationId
                }
                );
                return Ok(response);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteBookmarkRequest request)
        {
            try
            {
                var response = await _mediator.Send(new DeleteBookmarkCommand
                {
                    Id = request.Id
                });
                return Ok(response);
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}
