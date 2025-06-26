using Finme.Application.WebApp.Features.Account.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;        

        [HttpGet("photo/{userId}/{documentId}")]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Client, NoStore = false, VaryByHeader = "Accept")]
        public async Task<IActionResult> GetPhoto(int userId, int documentId)
        {
            var response = await _mediator.Send(new AccountDownloadCommand
            {
                UserId = userId,
                DocumentId = documentId
            });

            // Adiciona cabeçalho de cache
            Response.Headers.Remove("Content-Disposition");
            return File(response.FileStream, response.ContentType);
        }
    }
}
