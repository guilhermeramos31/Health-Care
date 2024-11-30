using HealthCare.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

builder.Services.AddScoped<HeathCareContext>();

// Connection whit DB
var postgreURL = "DB_URL";
var connection = builder.Configuration.GetConnectionString( postgreURL );
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<HeathCareContext>( option => option.UseNpgsql( connection ) );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
