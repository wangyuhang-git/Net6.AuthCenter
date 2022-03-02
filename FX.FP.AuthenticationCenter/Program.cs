using FX.FP.AuthenticationCenter.Business;
using FX.FP.AuthenticationCenter.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    //.NET6 Ĭ�ϵ�ϵ�л��������õ� System.Text.Json���޷�ʹ��[FromBody]
    //Nuget��װMicrosoft.AspNetCore.Mvc.NewtonsoftJson
    //����AddNewtonsoftJson������ʹ��[FromBody]
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ʹ��sql server���ݿ�
builder.Services.AddDbContext<AuthDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"))
    );

//��ȡtoken����
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));

//ע��Գƿ�����ܷ���
builder.Services.AddTransient<IAuthService, HSA_AuthService>();

////ע��ǶԳƲ�������ܷ���
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
