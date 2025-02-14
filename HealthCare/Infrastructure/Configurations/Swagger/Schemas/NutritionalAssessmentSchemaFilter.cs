using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HealthCare.Infrastructure.Configurations.Swagger.Schemas;

public class NutritionalAssessmentSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var nutritionalAssessmentExamples = new Dictionary<string, IOpenApiAny>
        {
            { "cb", new OpenApiString("CB123") },
            { "aj", new OpenApiString("AJ456") },
            { "cp", new OpenApiString("CP789") },
            { "estimatedWeight", new OpenApiFloat(70.5f) },
            { "estimatedStature", new OpenApiFloat(1.75f) },
            { "imc", new OpenApiFloat(22.86f) }
        };

        foreach (var property in schema.Properties)
        {
            if (nutritionalAssessmentExamples.TryGetValue(property.Key, out var example))
            {
                property.Value.Example = example;
            }
        }
    }
}