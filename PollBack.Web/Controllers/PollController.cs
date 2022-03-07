using Microsoft.AspNetCore.Mvc;
using PollBack.Infrastructure.Data.Interfaces;

namespace PollBack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PollController : ControllerBase
    {
        private readonly IPollRepository pollRepository;

        public PollController(IPollRepository pollRepository)
        {
            this.pollRepository = pollRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetManyAsync()
        {
            IEnumerable<Core.PollAggregate.Poll>? data = await pollRepository.GetManyAsync();

            return Ok(data);
        }
    }
}
