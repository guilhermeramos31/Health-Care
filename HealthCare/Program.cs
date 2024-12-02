using HealthCare.Context;
using HealthCare.Models.EntityEmployee;
using HealthCare.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Connection whit DB
var postgreURL = "DB_URL";
var connection = builder.Configuration.GetConnectionString( postgreURL );
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<HeathCareContext>( option => option.UseNpgsql( connection ) );
builder.Services.AddScoped<HeathCareContext>();

//EF
builder.Services.AddIdentity<Employee, EntityRole>()
    .AddEntityFrameworkStores<HeathCareContext>()
    .AddDefaultTokenProviders();


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
