using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserAuthenticationTest;
using UserAuthenticationTest.DAL;
using UserAuthenticationTest.Extensions;
using UserAuthenticationTest.Middleware;
using UserAuthenticationTest.Services;
using UserAuthenticationTest.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("BearerToken", new OpenApiSecurityScheme
    {
        Name = "Bearer",
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "To get the bearer token use endpoint /useraccount/generate-token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http
    });
    options.OperationFilter<AuthorizeCheckOperationFilter>();
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("MyDatabase"));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.RegisterJwtToken(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseMiddleware<HttpContextMiddleware>();
app.MapControllers();
app.UseAuthorization();

app.Run();
