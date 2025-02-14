using HealthCare.Services;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HealthCare.Models.Profiles;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.RoleEntity;
using HealthCare.Repositories;
using HealthCare.Infrastructure.Configurations.Role;
using HealthCare.Infrastructure.Configurations.Authentication;
using HealthCare.Infrastructure.Configurations.Swagger;
using HealthCare.Infrastructure.Data.Context;
using HealthCare.Infrastructure.Managers;
using HealthCare.Infrastructure.Managers.Interfaces;
using HealthCare.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSettings"));

//Mappers
builder.Services.AddAutoMapper(typeof(EmployeeProfile));
builder.Services.AddAutoMapper(typeof(RoleProfile));

builder.Services.AddScoped<HealthCareContext>();
builder.Services.AddScoped<IRepositoryUow, RepositoryUow>();
builder.Services.AddScoped<IManagerUow, ManagerUow>();
builder.Services.AddScoped<IServiceUow, ServiceUow>();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection whit DB
builder.Services.AddDbContext<HealthCareContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Authentication
builder.Services.AddAuthenticationJwt();

builder.Services.BuildSwagger(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger(configuration);
}

await app.CreateRoles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();