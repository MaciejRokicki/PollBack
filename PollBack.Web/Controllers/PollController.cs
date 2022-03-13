using MediatR;
using Microsoft.AspNetCore.Mvc;
using PollBack.Core.PollAggregate;
using PollBack.Core.PollAggregate.Commands;
using PollBack.Core.PollAggregate.Queries;
using PollBack.Web.Attributes;

namespace PollBack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PollController : ControllerBase
    {
        private readonly IMediator mediator;

        public PollController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [JwtAuthorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetManyAsync()
        {
            GetAllPollsQuery? query = new();

            IEnumerable<Poll>? response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePollCommand createPollCommand)
        {
            Poll? response = await mediator.Send(createPollCommand);

            return Ok(response);
        }
    }
}
