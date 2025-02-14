namespace HealthCare.Infrastructure.Configurations.Authentication;

public class JwtSetting
{
    public string SecretKey { get; init; } = string.Empty;
    public IList<string> Issuer { get; init; } = [];
    public string Audience { get; init; } = string.Empty;
}