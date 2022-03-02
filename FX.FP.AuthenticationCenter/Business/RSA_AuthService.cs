using FX.FP.AuthenticationCenter.Data.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FX.FP.AuthenticationCenter.Business
{
    public class RSA_AuthService : IAuthService
    {
        #region Option注入
        private readonly TokenOptions _tokenOptions;
        public RSA_AuthService(IOptionsMonitor<TokenOptions> tokenOptions)
        {
            this._tokenOptions = tokenOptions.CurrentValue;
        }
        #endregion

        public string GetToken(APIKeyInfo info)
        {
            #region 使用加密解密Key非对称 
            string keyDir = Directory.GetCurrentDirectory();
            if (RSAHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams) == false)
            {
                keyParams = RSAHelper.GenerateAndSaveKey(keyDir);
            }
            #endregion

            //有效载荷，尽量避免敏感信息
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, info.AppID),
                new Claim(ClaimTypes.Role,"Customer"),
                new Claim("Area",info.Area),
                new Claim("AllowIps",info.AllowIps),
                new Claim("DateTime",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            SigningCredentials credentials = new SigningCredentials(new RsaSecurityKey(keyParams), SecurityAlgorithms.RsaSha256Signature);

            var token = new JwtSecurityToken(
               issuer: this._tokenOptions.Issuer,
               audience: this._tokenOptions.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(60),//60分钟有效期
               signingCredentials: credentials);

            var handler = new JwtSecurityTokenHandler();
            string tokenString = handler.WriteToken(token);
            return tokenString;
        }
    }
}
