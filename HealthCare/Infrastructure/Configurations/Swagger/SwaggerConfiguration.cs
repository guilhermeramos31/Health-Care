using HealthCare.Infrastructure.Configurations.Swagger.Schemas;
using HealthCare.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace HealthCare.Infrastructure.Configurations.Swagger;

public static class SwaggerConfiguration
{
    private static void SwaggerGen(IServiceCollection service, IConfiguration configuration)
    {
        var swaggerSettings = configuration.GetSettings<SwaggerSetting>("Swagger");
        service.AddSwaggerGen(options =>
        {
            var securityScheme = new OpenApiSecurityScheme()
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            });

            options.SwaggerDoc(swaggerSettings.Version,
                new OpenApiInfo
                {
                    Title = swaggerSettings.Title, Version = swaggerSettings.Version,
                    Description = swaggerSettings.Description
                });

            options.SchemaFilter<EmployeeSchemaFilter>();
            options.SchemaFilter<HealthSituationSchemaFilter>();
            options.SchemaFilter<MedicationSchemaFilter>();
            options.SchemaFilter<PatientSchemaFilter>();
            options.SchemaFilter<NutritionalAssessmentSchemaFilter>();
            options.SchemaFilter<AddressSchemaFilter>();
            options.UseInlineDefinitionsForEnums();
        });
    }

    public static void BuildSwagger(this IServiceCollection service, IConfiguration configuration)
    {
        service
            .AddOpenApi()
            .AddEndpointsApiExplorer();
        SwaggerGen(service, configuration);
    }

    public static void UseCustomSwagger(this WebApplication app, IConfiguration configuration)
    {
        var swaggerSettings = configuration.GetSettings<SwaggerSetting>("Swagger");

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapSwagger();
            app
                .UseSwagger(options => { options.RouteTemplate = $"swagger/{{documentName}}/swagger.json"; })
                .UseSwaggerUI(options =>
                {
                    options.RoutePrefix = swaggerSettings.Prefix;
                    options.SwaggerEndpoint($"/swagger/{swaggerSettings.Version}/swagger.json", swaggerSettings.Title);
                });
        }
    }
}