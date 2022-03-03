using FX.FP.WebApi.Common.Authentication.Model;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FX.FP.WebApi.Common.Authentication
{
    /// <summary>
    /// 解析Jwt的Token(有效载荷部分)实现类
    /// </summary>
    public class AnalysisAuthentication : IAnalysisAuthentication
    {
        private readonly TokenOptions _options;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AnalysisAuthentication(
            IOptions<TokenOptions> options,
            IHttpContextAccessor httpContextAccessor
            )
        {
            this._options = options.Value;
            this._httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 解析JWT的token(有效载荷部分)
        /// 方式一：根据IHttpContextAccessor获取相关载荷信息
        /// </summary>
        /// <returns></returns>
        public CustomClaim GetCustomClaim()
        {
            CustomClaim customClaim = new CustomClaim()
            {
                exp = this._httpContextAccessor.HttpContext?.User?.FindFirst("exp")?.Value,
                AllowIps = this._httpContextAccessor.HttpContext?.User?.FindFirst("AllowIps")?.Value,
                Date = this._httpContextAccessor.HttpContext?.User?.FindFirst("Date")?.Value,
                Area = this._httpContextAccessor.HttpContext?.User?.FindFirst("Area")?.Value,
                aud = this._httpContextAccessor.HttpContext?.User?.FindFirst("aud")?.Value,
                iss = this._httpContextAccessor.HttpContext?.User?.FindFirst("iss")?.Value
            };
            return customClaim;
        }

        /// <summary>
        /// 解析JWT的token(有效载荷部分)
        /// 方式一：解析控制器中的base.HttpContext获取的token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public CustomClaim GetCustomClaim(string token)
        {
            CustomClaim customClaim = new CustomClaim();
            var tokenStr = this.TokenDecode(token);
            if (!string.IsNullOrEmpty(tokenStr))
            {
                customClaim = JsonConvert.DeserializeObject<CustomClaim>(tokenStr) ?? new CustomClaim();
            }
            return customClaim;
        }


        /// <summary>
        /// 解析Jwt的Token(有效载荷部分)为json格式的字符串
        /// </summary>
        /// <param name="token">鉴权所需要的toke</param>
        /// <returns></returns>
        public string TokenDecode(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return "token不能为空";
                }
                var json = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())////设置JWT算法
                    .WithSecret(this._options.SecurityKey)//校验的秘钥
                    .MustVerifySignature()//必须校验秘钥
                    .Decode(token);//解析token
                                   //Console.WriteLine($"解析后的json为：[{json}]");
                return json;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("解析token时出现异常：" + ex.ToString());
                return "";
            }
        }
    }
}
