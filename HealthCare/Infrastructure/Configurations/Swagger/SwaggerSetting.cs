namespace HealthCare.Infrastructure.Configurations.Swagger;

public abstract class SwaggerSetting
{
    public string Title { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Prefix { get; set; } = string.Empty;
}