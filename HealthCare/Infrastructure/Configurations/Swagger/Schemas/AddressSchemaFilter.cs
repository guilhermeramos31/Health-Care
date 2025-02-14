using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HealthCare.Infrastructure.Configurations.Swagger.Schemas;

public class AddressSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var addressExamples = new Dictionary<string, IOpenApiAny>
        {
            { "street", new OpenApiString("Main Street") },
            { "number", new OpenApiString("123") },
            { "complement", new OpenApiString("Apartment 4B") },
            { "neighborhood", new OpenApiString("Downtown") },
            { "city", new OpenApiString("São Paulo") },
            { "state", new OpenApiInteger(24) },
            { "zipcode", new OpenApiString("01311-000") },
            { "landmark", new OpenApiString("Near the Central Park") },
            { "addresstype", new OpenApiString("Residential") }
        };

        foreach (var property in schema.Properties)
        {
            if (addressExamples.TryGetValue(property.Key.ToLower(), out var example))
            {
                property.Value.Example = example;
            }
        }
    }
}