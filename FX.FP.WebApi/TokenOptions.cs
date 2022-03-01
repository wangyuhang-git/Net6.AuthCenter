namespace FX.FP.WebApi
{
    public class TokenOptions
    {
        /// <summary>
        /// 读者
        /// </summary>
        public string Audience
        {
            get;
            set;
        }
        /// <summary>
        /// 安全密钥
        /// </summary>
        public string SecurityKey
        {
            get;
            set;
        }
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer
        {
            get;
            set;
        }
    }
}
