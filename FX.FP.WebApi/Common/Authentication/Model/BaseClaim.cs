namespace FX.FP.WebApi.Common.Authentication.Model
{
    public class BaseClaim
    {
        /// <summary>
        /// token过期时间
        /// </summary>
        public string? exp { get; set; }

        /// <summary>
        /// 发行人
        /// </summary>
        public string? iss { get; set; }

        /// <summary>
        /// 读取者
        /// </summary>
        public string? aud { get; set; }


    }
}
