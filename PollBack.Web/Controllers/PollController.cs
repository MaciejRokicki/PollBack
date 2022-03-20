using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PollBack.Core.PollAggregate;
using PollBack.Core.PollAggregate.Commands;
using PollBack.Core.PollAggregate.Queries;
using PollBack.Web.Attributes;
using PollBack.Web.ViewModels;
using System.Security.Claims;

namespace PollBack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PollController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public PollController(
            IMediator mediator, 
            IHttpContextAccessor httpContextAccessor, 
            IMapper mapper)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreatePollCommand createPollCommand)
        {
            try
            {
                string? userId = httpContextAccessor.HttpContext?.User.FindFirstValue("UserId");

                createPollCommand.Model.UserId = userId != null ? int.Parse(userId) : null;

                Poll poll = await mediator.Send(createPollCommand);
                PollViewModel pollViewModel = mapper.Map<PollViewModel>(poll);

                return Ok(pollViewModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [JwtAuthorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> SetActive([FromBody] ActivatePollCommand activatePollCommand)
        {
            try
            {
                string? userId = httpContextAccessor.HttpContext?.User.FindFirstValue("UserId");

                if(userId != null)
                {
                    activatePollCommand.UserId = int.Parse(userId);

                    await mediator.Send(activatePollCommand);

                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [JwtAuthorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdatePollCommand updatePollCommand)
        {
            try
            {
                string? userId = httpContextAccessor.HttpContext?.User.FindFirstValue("UserId");

                if (userId != null)
                {
                    updatePollCommand.UserId = int.Parse(userId);

                    await mediator.Send(updatePollCommand);

                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                GetPollByIdQuery query = new GetPollByIdQuery(id);

                Poll poll = await mediator.Send(query);
                PollViewModel pollViewModel = mapper.Map<PollViewModel>(poll);

                return Ok(pollViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [JwtAuthorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMyPolls()
        {
            try
            {
                string? userId = httpContextAccessor.HttpContext?.User.FindFirstValue("UserId");

                if(userId != null)
                {
                    GetUserPollsQuery query = new(int.Parse(userId));

                    IEnumerable<Poll> polls = await mediator.Send(query);
                    IEnumerable<PollViewModel> pollViewModels = mapper.Map<IEnumerable<PollViewModel>>(polls);

                    return Ok(pollViewModels);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Vote([FromBody] VotePollCommand votePollCommand)
        {
            try
            {
                string? userId = httpContextAccessor.HttpContext?.User.FindFirstValue("UserId");

                votePollCommand.UserId = userId != null ? int.Parse(userId) : null;

                await mediator.Send(votePollCommand);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
