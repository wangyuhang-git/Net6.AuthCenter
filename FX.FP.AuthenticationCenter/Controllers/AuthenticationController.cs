using FX.FP.AuthenticationCenter.Business;
using FX.FP.AuthenticationCenter.Common;
using FX.FP.AuthenticationCenter.Data;
using FX.FP.AuthenticationCenter.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FX.FP.AuthenticationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthService _authService;
        private readonly AuthDbContext _authDbContext;
        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IAuthService authService,
            AuthDbContext authDbContext)
        {
            _logger = logger;
            _authService = authService;
            _authDbContext = authDbContext;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello！！！";
        }

        [HttpPost]
        public JsonResult Post([FromBody] JObject value)
        {
            ResultInfo resultInfo = new ResultInfo();
            dynamic entity = value;
            if (entity != null)
            {
                APIKeyInfo apiKeyInfo = JsonConvert.DeserializeObject<APIKeyInfo>(entity.ToString());
                if (string.IsNullOrEmpty(apiKeyInfo.AppID) || string.IsNullOrEmpty(apiKeyInfo.AppSecret))
                {
                    resultInfo = new ResultInfo
                    {
                        Success = false,
                        Msg = "AppID和AppSecret参数不能为空！",
                        Token = ""
                    };
                }
                else
                {
                    var info = _authDbContext.APIKeyInfos.FirstOrDefault(c => c.AppID == apiKeyInfo.AppID && c.AppSecret == apiKeyInfo.AppSecret);
                    if (null == info)
                    {
                        resultInfo = new ResultInfo
                        {
                            Success = false,
                            Msg = "AppID和AppSecret验证失败！",
                            Token = ""
                        };
                    }
                    else
                    {
                        string token = _authService.GetToken(info);
                        resultInfo = new ResultInfo
                        {
                            Success = true,
                            Msg = "获取成功！",
                            Token = token
                        };
                    }
                }
            }
            else
            {
                resultInfo = new ResultInfo
                {
                    Success = false,
                    Msg = "参数不能为空！",
                    Token = ""
                };
            }
            return new JsonResult(resultInfo);
        }
    }
}
