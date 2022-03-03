using FX.FP.WebApi;
using FX.FP.WebApi.Common.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//注册HttpContext读取器
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


//配置后可以在构造函数中用注入（IOptions<TokenOptions>）的形式使用
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));


//将配置信息直接读取到实体类中
TokenOptions tokenOptions = new TokenOptions();
builder.Configuration.Bind("TokenOptions", tokenOptions);

//JWT校验(HSA)
builder.Services.AddHSAAuthBuilder(tokenOptions);

////JWT校验(RSA)
//builder.Services.AddRSAAuthBuilder(tokenOptions);

//注入单例服务，将其作为IAuthorizationMiddlewareResultHandler的实现。
//修改Authorization返回自定义json格式进行友好提示
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();

//注册解析jwt的token(有效载荷部分)为json字符串的服务
builder.Services.AddTransient<IAnalysisAuthentication, AnalysisAuthentication>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();//鉴权，验证中心

app.UseAuthorization();

app.MapControllers();

app.Run();
