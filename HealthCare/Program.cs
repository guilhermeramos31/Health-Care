using HealthCare.Context;
using HealthCare.Services;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HealthCare.Models.Profiles;
using HealthCare.Models.EntityEmployee;
using HealthCare.Models.EntityRole;
using HealthCare.Repositories;
using HealthCare.Configurations.Jwt;
using HealthCare.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

//Mappers
builder.Services.AddAutoMapper( typeof( EmployeeProfile ) );

//ID
builder.Services.AddScoped<HeathCareContext>();
builder.Services.AddScoped<IRepositoryUow, RepositoryUow>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITokenService, TokenServices>();
builder.Services.AddScoped<IRoleService, RoleService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Connection whit DB
var postgreUrl = "DB_URL";
builder.Services.AddDbContext<HeathCareContext>( options =>
    options.UseNpgsql( builder.Configuration.GetConnectionString( postgreUrl ) ) );

//EF
builder.Services.AddIdentity<Employee, Role>( employee =>
{
    employee.Password.RequireDigit = true;
    employee.Password.RequireLowercase = true;
    employee.Password.RequireNonAlphanumeric = true;
    employee.Password.RequireUppercase = true;
    employee.Password.RequiredLength = 6;
    employee.User.RequireUniqueEmail = true;
}
)
    .AddRoleManager<RoleManager<Role>>()
    .AddSignInManager<SignInManager<Employee>>()
    .AddUserManager<UserManager<Employee>>()
    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Employee, Role>>()
    .AddEntityFrameworkStores<HeathCareContext>()
    .AddDefaultTokenProviders();

//Authentication
builder.AddAuthenticationJwt();

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
