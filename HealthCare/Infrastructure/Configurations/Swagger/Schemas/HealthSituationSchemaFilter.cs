using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HealthCare.Infrastructure.Configurations.Swagger.Schemas;

public class HealthSituationSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var healthSituationExamples = new Dictionary<string, IOpenApiAny>
        {
            { "shortcoming", new OpenApiString("Difficulty walking") },
            { "bedridden", new OpenApiBoolean(true) },
            { "wheelchairUser", new OpenApiBoolean(false) },
            { "wanders", new OpenApiBoolean(true) },
            { "comorbidities", new OpenApiString("Hypertension, Diabetes") },
            { "historic", new OpenApiString("History of stroke in 2020") }
        };

        ApplyExamplesToSchema(schema, healthSituationExamples);
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