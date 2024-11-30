using HealthCare.Context;
using HealthCare.Models.Employee;
using HealthCare.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

//EF
builder.Services.AddIdentity<Employee, Role>()
    .AddEntityFrameworkStores<HeathCareContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<HeathCareContext>();

// Connection whit DB
var postgreURL = "DB_URL";
var connection = builder.Configuration.GetConnectionString( postgreURL );
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<HeathCareContext>( option => option.UseNpgsql( connection ) );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
