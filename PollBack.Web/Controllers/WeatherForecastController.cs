using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace PollBack.Web.Controllers
{
    public class G
    {
        public string s { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly G g;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<G> g)
        {
            _logger = logger;
            this.g = g.Value;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("[action]")]
        public G Getttt()
        {
            return g;
        }
    }
}