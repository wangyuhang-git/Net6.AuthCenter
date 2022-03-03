using Microsoft.AspNetCore.Authorization.Policy;

namespace Microsoft.AspNetCore.Authorization
{
    /// <summary>
    /// Authorize认证及授权自定义自定义返回json格式
    /// </summary>
    public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            //因为管道还没有走到Action 所以没有ActionResult使用 必须自己定义Response中的内容
            //如果授权不成功 验证是否鉴权（身份验证）通过 如果鉴权通过的则是权限不够
            if (!authorizeResult.Succeeded)
            {
                //将状态码定义为200
                context.Response.StatusCode = 200;
                //使用 WriteAsJsonAsync 写入一个自定义的返回对象 自动完成Json的序列化操作
                //这里用匿名类 自定义状态码和提示信息
                //身份验证是否通过
                if (!context.User.Identity.IsAuthenticated)
                    await context.Response.WriteAsJsonAsync(new { Code = StatusCodes.Status401Unauthorized, Message = "身份验证不通过", Result = string.Empty });
                else
                    await context.Response.WriteAsJsonAsync(new { Code = StatusCodes.Status403Forbidden, Message = "没有权限", Result = string.Empty });
                //注意一定要return 在这里短路管道 不要走到next 否则线程会进入后续管道 到达action中
                return;
            }
            //如果授权成功 继续执行后续的中间件 记住一定记得next 否则会管道会短路
            await next(context);
        }
    }
}
