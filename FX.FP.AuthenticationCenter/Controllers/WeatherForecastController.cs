using FX.FP.AuthenticationCenter.Data;
using FX.FP.AuthenticationCenter.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FX.FP.AuthenticationCenter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly AuthDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, AuthDbContext authDbContext)
        {
            _logger = logger;
            _context = authDbContext;
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

        [HttpPost(Name = "GetList")]
        public async Task<ActionResult<APIKeyInfo>> GetList(string guid)
        {
            return await _context.APIKeyInfos.FindAsync(guid) ?? new APIKeyInfo();
        }
    }
}