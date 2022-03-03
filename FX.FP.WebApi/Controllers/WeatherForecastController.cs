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
        [AllowAnonymous]//用于跳过AuthorizeAttribute认证
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
            //获取claims
            var claims = base.HttpContext.AuthenticateAsync().Result.Principal?.Claims.ToList();
            //获取请求的token
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
        ///// 解析JWT
        ///// </summary>
        ///// <param name="token"></param>
        //public string JWTDecode(string token)
        //{
        //    try
        //    {
        //        var json = new JwtBuilder()
        //        .WithAlgorithm(new HMACSHA256Algorithm()) //设置JWT算法
        //        .WithSecret(_tokenOptions.SecurityKey)//校验的秘钥
        //        .MustVerifySignature()//必须校验秘钥
        //        .Decode(token);//解析token
        //        //输出解析后的json
        //        Console.WriteLine($"方式二解析后的json为：[{json}]");
        //        return json;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("解析json时出现异常：" + ex.ToString());
        //        return "";

        //    }

        //}

    }
}