﻿using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HealthCare.Infrastructure.Configurations.Swagger.Schemas;

public abstract class SchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var propertyExamples = new Dictionary<string, IOpenApiAny>
        {
            { "userName", new OpenApiString("johndoe") },
            { "name", new OpenApiString("John Doe") },
            { "email", new OpenApiString("john.doe@example.com") },
            { "password", new OpenApiString("SafePassword123@") },
            { "phoneNumber", new OpenApiString("+5511999999999") },
            { "receiver", new OpenApiString("john.doe@example.com") },
            { "amount", new OpenApiDouble(10)}
        };

        foreach (var property in schema.Properties)
        {
            if (propertyExamples.TryGetValue(property.Key, out var example))
            {
                property.Value.Example = example;
            }
        }
    }
}