using FX.FP.WebApi.Common.Authentication.Model;

namespace FX.FP.WebApi.Common.Authentication
{
    /// <summary>
    /// 解析Jwt的Token(有效载荷部分)接口
    /// </summary>
    public interface IAnalysisAuthentication
    {
        /// <summary>
        /// 得到解析JWT的token(有效载荷部分)json格式字符串
        /// </summary>
        /// <param name="token">鉴权所需要的token</param>
        /// <returns></returns>
        public string TokenDecode(string token);

        /// <summary>
        /// 解析JWT的token(有效载荷部分)
        /// </summary>
        /// <returns></returns>
        public CustomClaim GetCustomClaim();

        /// <summary>
        /// 解析JWT的token(有效载荷部分)
        /// </summary>
        /// <param name="token">鉴权所需要的token</param>
        /// <returns></returns>
        public CustomClaim GetCustomClaim(string token);
    }
}
