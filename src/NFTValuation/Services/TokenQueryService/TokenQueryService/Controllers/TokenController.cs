using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenQueryService.Application.Commands;

namespace TokenQueryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetToken(string tokenId, string tokenIndex, string contractAddress)
        {
            var command = new TokenQueryCommand(tokenId, tokenIndex, contractAddress);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
