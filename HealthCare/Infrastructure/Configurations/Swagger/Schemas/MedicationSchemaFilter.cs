using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HealthCare.Infrastructure.Configurations.Swagger.Schemas;

public class MedicationSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var medicationExamples = new Dictionary<string, IOpenApiAny>
        {
            { "name", new OpenApiString("Paracetamol") },
            { "period", new OpenApiInteger(7) },
            { "quantity", new OpenApiString("1 tablet") },
            { "time", new OpenApiString("08:00:00") },
            { "description", new OpenApiString("Take after meals") }
        };

        ApplyExamplesToSchema(schema, medicationExamples);
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