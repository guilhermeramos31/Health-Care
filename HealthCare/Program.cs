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
using HealthCare.Infrastructure.Data.Context;
using HealthCare.Infrastructure.Managers;
using HealthCare.Infrastructure.Managers.Interfaces;
using HealthCare.Repositories.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

//EF
builder.Services.AddIdentity<Employee, Role>(employee =>
    {
        employee.Password.RequireDigit = false;
        employee.Password.RequireLowercase = false;
        employee.Password.RequireUppercase = false;
        employee.Password.RequireNonAlphanumeric = false;
    })
    .AddRoleManager<RoleManager<Role>>()
    .AddUserManager<UserManager<Employee>>()
    .AddEntityFrameworkStores<HealthCareContext>()
    .AddDefaultTokenProviders();
//Authentication
builder.Services.AddAuthenticationJwt();

builder.Services.AddSwaggerGen(options =>
{
    var bearer = "Bearer";
    options.AddSecurityDefinition(bearer, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = bearer,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = bearer
                }
            },
            []
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.CreateRoles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();