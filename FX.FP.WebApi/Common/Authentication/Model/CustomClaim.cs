namespace FX.FP.WebApi.Common.Authentication.Model
{
    public class CustomClaim : BaseClaim
    {
        /// <summary>
        /// 地区
        /// </summary>
        public string? Area { get; set; }

        /// <summary>
        /// token颁发时间
        /// </summary>
        public string? Date { get; set; }

        /// <summary>
        /// 允许访问的ip
        /// </summary>
        public string? AllowIps { get; set; }
    }
}
