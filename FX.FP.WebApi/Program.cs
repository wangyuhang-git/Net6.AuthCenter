using FX.FP.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));
TokenOptions tokenOptions = new TokenOptions();
builder.Configuration.Bind("TokenOptions", tokenOptions);

////JWT校验(HSA)
//builder.Services.AddHSAAuthBuilder(tokenOptions);

//JWT校验(RSA)
builder.Services.AddRSAAuthBuilder(tokenOptions);


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
