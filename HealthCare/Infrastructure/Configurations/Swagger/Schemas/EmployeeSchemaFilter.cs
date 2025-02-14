using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HealthCare.Infrastructure.Configurations.Swagger.Schemas;

public class EmployeeSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var employeeExamples = new Dictionary<string, IOpenApiAny>
        {
            { "userName", new OpenApiString("johndoe") },
            { "name", new OpenApiString("John Doe") },
            { "email", new OpenApiString("john.doe@example.com") },
            { "password", new OpenApiString("SafePassword123@") },
            { "phoneNumber", new OpenApiString("+5511999999999") },
            { "status", new OpenApiString("Active") },
            { "observations", new OpenApiString("No issues reported.") },
            { "role", new OpenApiInteger(0) }
        };

        ApplyExamplesToSchema(schema, employeeExamples);
    }

    private void ApplyExamplesToSchema(OpenApiSchema schema, Dictionary<string, IOpenApiAny> propertyExamples)
    {
        foreach (var property in schema.Properties)
        {
            if (propertyExamples.TryGetValue(property.Key, out var example))
            {
                property.Value.Example = example;
            }
        }
    }
}