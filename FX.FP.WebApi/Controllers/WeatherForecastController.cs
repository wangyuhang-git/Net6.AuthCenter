using FX.FP.WebApi.Common.Authentication;
using FX.FP.WebApi.Common.Authentication.Model;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FX.FP.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        //private readonly TokenOptions _tokenOptions;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAnalysisAuthentication _analysis;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IOptions<TokenOptions> tokenOptions,
            IAnalysisAuthentication analysis)
        {
            //_tokenOptions = tokenOptions.Value;
            _logger = logger;
            _analysis = analysis;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [AllowAnonymous]//��������AuthorizeAttribute��֤
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

        [HttpPost]
        [Authorize]
        public string Post()
        {
            //��ȡclaims
            var claims = base.HttpContext.AuthenticateAsync().Result.Principal?.Claims.ToList();
            //��ȡ�����token
            //var token = base.HttpContext.AuthenticateAsync().Result.Properties?.Items.ToArray()[1].Value;
            var token = base.HttpContext.GetTokenAsync("access_token").Result;

            return _analysis.TokenDecode(token);
        }

        [HttpPost("TokenClaim")]
        [Authorize]
        public CustomClaim TokenClaim()
        {
            var token = base.HttpContext.GetTokenAsync("access_token").Result;

            return _analysis.GetCustomClaim(token);
        }

        [HttpPost("Claim")]
        [Authorize]
        public CustomClaim Claim()
        {
            return _analysis.GetCustomClaim();
        }

        ///// <summary>
        ///// ����JWT
        ///// </summary>
        ///// <param name="token"></param>
        //public string JWTDecode(string token)
        //{
        //    try
        //    {
        //        var json = new JwtBuilder()
        //        .WithAlgorithm(new HMACSHA256Algorithm()) //����JWT�㷨
        //        .WithSecret(_tokenOptions.SecurityKey)//У�����Կ
        //        .MustVerifySignature()//����У����Կ
        //        .Decode(token);//����token
        //        //����������json
        //        Console.WriteLine($"��ʽ���������jsonΪ��[{json}]");
        //        return json;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("����jsonʱ�����쳣��" + ex.ToString());
        //        return "";

        //    }

        //}

    }
}