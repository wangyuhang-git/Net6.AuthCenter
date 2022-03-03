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

//ע��HttpContext��ȡ��
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


//���ú�����ڹ��캯������ע�루IOptions<TokenOptions>������ʽʹ��
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));


//��������Ϣֱ�Ӷ�ȡ��ʵ������
TokenOptions tokenOptions = new TokenOptions();
builder.Configuration.Bind("TokenOptions", tokenOptions);

//JWTУ��(HSA)
builder.Services.AddHSAAuthBuilder(tokenOptions);

////JWTУ��(RSA)
//builder.Services.AddRSAAuthBuilder(tokenOptions);

//ע�뵥�����񣬽�����ΪIAuthorizationMiddlewareResultHandler��ʵ�֡�
//�޸�Authorization�����Զ���json��ʽ�����Ѻ���ʾ
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();

//ע�����jwt��token(��Ч�غɲ���)Ϊjson�ַ����ķ���
builder.Services.AddTransient<IAnalysisAuthentication, AnalysisAuthentication>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();//��Ȩ����֤����

app.UseAuthorization();

app.MapControllers();

app.Run();
