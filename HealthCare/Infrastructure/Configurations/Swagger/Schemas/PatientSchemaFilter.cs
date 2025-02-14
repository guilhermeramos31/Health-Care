using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace HealthCare.Infrastructure.Configurations.Swagger.Schemas;

public class PatientSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var patientExamples = new Dictionary<string, IOpenApiAny>
        {
            { "cns", new OpenApiString("123456789012345") },
            { "cpf", new OpenApiString("123.456.789-09") },
            { "dateOfBirth", new OpenApiDate(new DateTime(1980, 1, 1)) },
            { "maritalStatus", new OpenApiString("Married") },
            { "nationality", new OpenApiString("Brazilian") },
            { "rg", new OpenApiString("12.345.678-9") }
        };

        ApplyExamplesToSchema(schema, patientExamples);
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