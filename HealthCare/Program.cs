using HealthCare.Configurations;
using HealthCare.Context;
using HealthCare.Models.EntityEmployee;
using HealthCare.Models.Role;
using HealthCare.Services;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<ITokenService, TokenServices>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Connection whit DB
var postgreURL = "DB_URL";
var connection = builder.Configuration.GetConnectionString( postgreURL );
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<HeathCareContext>( option => option.UseNpgsql( connection ) );
builder.Services.AddScoped<HeathCareContext>();

//EF
builder.Services.AddIdentity<Employee, EntityRole>( employee => 
{
    employee.Password.RequireDigit = true;
    employee.Password.RequireLowercase = true;
    employee.Password.RequireNonAlphanumeric = true;
    employee.Password.RequireUppercase = true;
    employee.Password.RequiredLength = 6;
    employee.User.RequireUniqueEmail = true;
}
)
    .AddRoleManager<RoleManager<EntityRole>>()
    .AddSignInManager<SignInManager<Employee>>()
    .AddUserManager<UserManager<Employee>>()
    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Employee, EntityRole>>()
    .AddEntityFrameworkStores<HeathCareContext>()
    .AddDefaultTokenProviders();

//Authentication
var jwtSettings = builder.Configuration.GetSection( "JwtSettings" ).Get<JwtSettings>()
                 ?? throw new InvalidOperationException( "JWT settings are not configured." );

builder.Services.AddAuthentication( options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
} ).AddJwtBearer( options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration[ jwtSettings.Issuer ],
        ValidAudience = builder.Configuration[ jwtSettings.Audience ],
        IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( jwtSettings.SecretKey ) )
    };
} );

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
