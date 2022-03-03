using FX.FP.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// JWT加密方式 创建者扩展
    /// </summary>
    public static class AuthenticationExt
    {
        /// <summary>
        /// 注册HSA对称可逆加密
        /// </summary>
        /// <param name="services"></param>
        /// <param name="tokenOptions"></param>
        public static void AddHSAAuthBuilder(this IServiceCollection services, TokenOptions tokenOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = tokenOptions.Audience,//
                    ValidIssuer = tokenOptions.Issuer,//Issuer，Audience这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                };
            });
        }

        /// <summary>
        /// 注册RSA非对称不可逆加密
        /// </summary>
        /// <param name="services"></param>
        /// <param name="tokenOptions"></param>
        public static void AddRSAAuthBuilder(this IServiceCollection services, TokenOptions tokenOptions)
        {
            // 读取公钥
            string path = Path.Combine(Directory.GetCurrentDirectory(), "key.public.json");
            string key = File.ReadAllText(path);//this.Configuration["SecurityKey"];
            //Console.WriteLine($"KeyPath:{path}");

            var keyParams = JsonConvert.DeserializeObject<RSAParameters>(key);
            //SigningCredentials credentials = new SigningCredentials(new RsaSecurityKey(keyParams), SecurityAlgorithms.RsaSha256Signature);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = tokenOptions.Audience,//Audience
                    ValidIssuer = tokenOptions.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new RsaSecurityKey(keyParams)
                };
            });
        }
    }
}
