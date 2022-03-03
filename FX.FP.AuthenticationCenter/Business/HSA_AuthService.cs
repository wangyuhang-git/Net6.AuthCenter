using FX.FP.AuthenticationCenter.Data.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FX.FP.AuthenticationCenter.Business
{
    public class HSA_AuthService : IAuthService
    {
        #region Option注入
        private readonly TokenOptions _tokenOptions;
        public HSA_AuthService(IOptionsMonitor<TokenOptions> tokenOptions)
        {
            this._tokenOptions = tokenOptions.CurrentValue;
        }
        #endregion
        /// <summary>
        /// 用户登录成功以后，用来生成Token的方法
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public string GetToken(APIKeyInfo info)
        {
            #region 有效载荷，尽量避免敏感信息
            var claims = new[]
          {
               new Claim(ClaimTypes.Name, info.AppID),
               new Claim(ClaimTypes.Role,"Customer"),//传递其他信息  
               new Claim("Area",info.Area), //传递其他信息  
               new Claim("AllowIps",info.AllowIps),
               new Claim("Date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            //需要加密：需要加密key:
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
             issuer: _tokenOptions.Issuer,
             audience: _tokenOptions.Audience,
             claims: claims,
             expires: DateTime.Now.AddMinutes(60),//60分钟有效期
             signingCredentials: creds);

            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
            #endregion

        }
    }
}
