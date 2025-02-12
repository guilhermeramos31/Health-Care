namespace HealthCare.Infrastructure.Configurations.Jwt;

public class JwtBody
{
    public string SecretKey { get; init; } = string.Empty;
    public List<string> Issuer { get; init; } = [];
    public string Audience { get; init; } = string.Empty;
}