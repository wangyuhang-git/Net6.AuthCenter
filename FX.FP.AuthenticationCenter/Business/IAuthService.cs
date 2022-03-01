using FX.FP.AuthenticationCenter.Data.Models;

namespace FX.FP.AuthenticationCenter.Business
{
    public interface IAuthService
    {
        public string GetToken(APIKeyInfo info);
    }
}
