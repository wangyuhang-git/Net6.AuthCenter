using FX.FP.AuthenticationCenter.Business;
using FX.FP.AuthenticationCenter.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    //.NET6 默认的系列化库是内置的 System.Text.Json，无法使用[FromBody]
    //Nuget安装Microsoft.AspNetCore.Mvc.NewtonsoftJson
    //增加AddNewtonsoftJson，即可使用[FromBody]
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//使用sql server数据库
builder.Services.AddDbContext<AuthDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"))
    );

//读取token配置
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));

//注册对称可逆加密服务
builder.Services.AddTransient<IAuthService, HSA_AuthService>();

////注册非对称不可逆加密服务
//builder.Services.AddTransient<IAuthService, RSA_AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
